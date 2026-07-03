using ImageTransformer.Application.Models;
using ImageTransformer.Parameters;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ImageTransformer.Apis;

public static class ImageTransformerApi
{
    public static RouteHandlerBuilder MapImageTransformApi(this IEndpointRouteBuilder app)
    {
        var api = app.MapPost("/process/{transform}/{coords}", ProcessImage);

        return api;
    }

    private static async Task<Results<Ok, BadRequest, NoContent, ProblemHttpResult>> ProcessImage
    (
        HttpContext context,
        Transformation transform,
        Coordinates coords
    )
    {
        throw new NotImplementedException();
    }
}