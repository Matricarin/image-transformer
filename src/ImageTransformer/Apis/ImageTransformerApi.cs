using ImageTransformer.Services;
using ImageTransformer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using SkiaSharp;

namespace ImageTransformer.Apis;

public static class ImageTransformerApi
{
    private const long MaxFileSizeBytes = 100 * 1024;
    private const int MaxDimension = 1000;

    public static RouteHandlerBuilder MapImageTransformApi(this IEndpointRouteBuilder app)
    {
        var api = app.MapPost("/process/{transform}/{coords}", ProcessImage);

        return api;
    }

    private static async Task<IResult> ProcessImage
    (
        HttpContext context,
        [FromServices] ITransformationService transformationService,
        [FromServices] ICropService cropService,
        [FromServices] IValidationService validationService,
        string transform,
        string coords
    )
    {
        if (!validationService.ValidateParameters(transform, coords,
                out var transformation, out var coordinates))
        {
            ApplicationDiagnostics.HttpRequestsTotal.Add(1,
                new KeyValuePair<string, object?>("transform", transform),
                new KeyValuePair<string, object?>("status_code", 400));

            return Results.BadRequest();
        }

        if (context.Request.ContentLength > MaxFileSizeBytes)
        {
            ApplicationDiagnostics.HttpRequestsTotal.Add(1,
                new KeyValuePair<string, object?>("transform", transform),
                new KeyValuePair<string, object?>("status_code", 400));

            return Results.BadRequest();
        }

        using var memory = new MemoryStream();

        await context.Request.Body.CopyToAsync(memory);

        if (memory.Length > MaxFileSizeBytes)
        {
            ApplicationDiagnostics.HttpRequestsTotal.Add(1,
                new KeyValuePair<string, object?>("transform", transform),
                new KeyValuePair<string, object?>("status_code", 400));

            return Results.BadRequest();
        }

        memory.Position = 0;

        using (var skStream = new SKManagedStream(memory))
        {
            using (var codec = SKCodec.Create(skStream))
            {
                if (codec is null)
                {
                    ApplicationDiagnostics.HttpRequestsTotal.Add(1,
                        new KeyValuePair<string, object?>("transform", transform),
                        new KeyValuePair<string, object?>("status_code", 400));

                    return Results.BadRequest();
                }

                if (codec.EncodedFormat != SKEncodedImageFormat.Png)
                {
                    ApplicationDiagnostics.HttpRequestsTotal.Add(1,
                        new KeyValuePair<string, object?>("transform", transform),
                        new KeyValuePair<string, object?>("status_code", 400));

                    return Results.BadRequest();
                }

                var info = codec.Info;

                if (info.Width > MaxDimension || info.Height > MaxDimension)
                {
                    ApplicationDiagnostics.HttpRequestsTotal.Add(1,
                        new KeyValuePair<string, object?>("transform", transform),
                        new KeyValuePair<string, object?>("status_code", 400));

                    return Results.BadRequest();
                }

                if (!validationService.ValidateTransformation(info.Width, info.Height,
                        transformation.Type, coordinates))
                {
                    ApplicationDiagnostics.HttpRequestsTotal.Add(1,
                        new KeyValuePair<string, object?>("transform", transform),
                        new KeyValuePair<string, object?>("status_code", 204));

                    return Results.NoContent();
                }

                if (info.ColorType != SKColorType.Bgra8888)
                {
                    ApplicationDiagnostics.HttpRequestsTotal.Add(1,
                        new KeyValuePair<string, object?>("transform", transform),
                        new KeyValuePair<string, object?>("status_code", 400));

                    return Results.BadRequest();
                }
            }
        }

        memory.Position = 0;

        var transformedBytes = transformationService.Transform(transformation.Type, memory.ToArray());

        var croppedBitmap = cropService.Crop(coordinates, transformedBytes);

        ApplicationDiagnostics.HttpRequestsTotal.Add(1,
            new KeyValuePair<string, object?>("transform", transform),
            new KeyValuePair<string, object?>("status_code", 200));

        return Results.File(croppedBitmap, "image/png", "processed-image.png");
    }
}