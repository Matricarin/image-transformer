using ImageTransformer.Apis.Parameters;
using ImageTransformer.Models;
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
    //  TODO: check 
    private static async Task<IResult> ProcessImage
    (
        HttpContext context,
        [FromServices] ITransformationService transformationService,
        [FromServices] ICropService cropService,
        string transform,
        string coords
    )
    {
        if (transform is null)
        {
            return Results.BadRequest();
        }

        if (!Coordinates.TryParse(coords, out var coordinates))
        {
            return Results.BadRequest();
        }

        if (context.Request.ContentLength > MaxFileSizeBytes)
        {
            return Results.BadRequest();
        }

        using var memory = new MemoryStream();

        await context.Request.Body.CopyToAsync(memory);

        if (memory.Length > MaxFileSizeBytes)
        {
            return Results.BadRequest();
        }

        memory.Position = 0;

        using (var skStream = new SKManagedStream(memory))
        {
            using (var codec = SKCodec.Create(skStream))
            {
                if (codec is null)
                {
                    return Results.BadRequest();
                }

                var info = codec.Info;

                if (info.Width > MaxDimension || info.Height > MaxDimension)
                {
                    return Results.BadRequest();
                }

                if (info.ColorType != SKColorType.Bgra8888)
                {
                    return Results.BadRequest();
                }
            }
        }

        memory.Position = 0;

        var transformedBytes = transformationService.Transform(TransformationType.FlipHorizontally, memory);

        var croppedBitmap = cropService.Crop(coordinates, transformedBytes);

        return Results.File(croppedBitmap.ToArray());
    }
}