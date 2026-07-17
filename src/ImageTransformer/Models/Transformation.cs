namespace ImageTransformer.Models;

public sealed class Transformation
{
    public TransformationType Type { get; init; }

    public static bool TryParse(string value, out Transformation result)
    {
        if (string.IsNullOrEmpty(value))
        {
            result = null;
            return false;
        }

        switch (value)
        {
            case "rotate-cw":
            {
                result = new Transformation
                {
                    Type = TransformationType.RotateClockwise
                };
                break;
            }
            case "rotate-ccw":
            {
                result = new Transformation
                {
                    Type = TransformationType.RotateCounterClockwise
                };
                break;
            }
            case "flip-v":
            {
                result = new Transformation
                {
                    Type = TransformationType.FlipVertically
                };
                break;
            }
            case "flip-h":
            {
                result = new Transformation
                {
                    Type = TransformationType.FlipHorizontally
                };
                break;
            }
            default:
                result = null;
                return false;
        }

        return true;
    }
}