using ImageTransformer.Models;
using ImageTransformer.Services.Interfaces;

namespace ImageTransformer.Services;

public sealed class ValidationService : IValidationService
{
    public bool ValidateParameters(string transform, string coords, out Transformation transformation,
        out Coordinates coordinates)
    {
        coordinates = default;

        return Transformation.TryParse(transform, out transformation) &&
               Coordinates.TryParse(coords, out coordinates);
    }

    public bool ValidateTransformation(int width, int height, TransformationType transformation, Coordinates coordinates)
    {
        var transformedWidth = transformation switch
        {
            TransformationType.RotateClockwise => height,
            TransformationType.RotateCounterClockwise => height,
            TransformationType.FlipVertically => width,
            TransformationType.FlipHorizontally => width,
            _ => throw new ArgumentOutOfRangeException(nameof(transformation), transformation, null)
        };

        var transformedHeight = transformation switch
        {
            TransformationType.RotateClockwise => width,
            TransformationType.RotateCounterClockwise => width,
            TransformationType.FlipVertically => height,
            TransformationType.FlipHorizontally => height,
            _ => throw new ArgumentOutOfRangeException(nameof(transformation), transformation, null)
        };

        var endX = coordinates.X + coordinates.Width;
        var endY = coordinates.Y + coordinates.Height;

        if (coordinates.X < 0 && endX < 0)
        {
            return false;
        }

        if (coordinates.X > transformedWidth && endX > transformedWidth)
        {
            return false;
        }

        if (coordinates.Y < 0 && endY < 0)
        {
            return false;
        }

        if (coordinates.Y > transformedHeight && endY > transformedHeight)
        {
            return false;
        }

        return true;
    }
}