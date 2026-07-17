using ImageTransformer.Models;

namespace ImageTransformer.Services.Interfaces;

public interface IValidationService
{
    bool ValidateParameters
    (
        string transform,
        string coords,
        out Transformation transformation,
        out Coordinates coordinates
    );

    bool ValidateTransformation
    (
        int width,
        int height,
        TransformationType transformation,
        Coordinates coordinates
    );
}