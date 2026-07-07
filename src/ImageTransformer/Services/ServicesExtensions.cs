using ImageTransformer.Services.Interfaces;

namespace ImageTransformer.Services;

public static class ServicesExtensions
{
    public static IServiceCollection AddImageTransformServices(this IServiceCollection services)
    {
        services.AddTransient<ITransformationService, TransformationService>();
        services.AddTransient<ICropService, CropService>();
        return services;
    }
}