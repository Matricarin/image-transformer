using ImageTransformer.Apis.Parameters;
using ImageTransformer.Models;

namespace ImageTransformer.Apis;

public static class ImageTransformerApi
{
    public static RouteHandlerBuilder MapImageTransformApi(this IEndpointRouteBuilder app)
    {
        var api = app.MapPost("/process/{transform}/{coords}", ProcessImage);

        return api;
    }

    private static async Task<IResult> ProcessImage
    (
        HttpContext context,
        Transformation? transform,
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

        using (var memory = new MemoryStream())
        {
            await context.Request.Body.CopyToAsync(memory);
        }
    }
}