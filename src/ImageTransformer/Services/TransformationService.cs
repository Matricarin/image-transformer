using ImageTransformer.Models;
using ImageTransformer.Services.Interfaces;
using SkiaSharp;

namespace ImageTransformer.Services;

public sealed class TransformationService : ITransformationService
{
    private const int NinetyDegrees = 90;

    public ReadOnlySpan<byte> Transform(TransformationType transformation, ReadOnlySpan<byte> bitmapBytes)
    {
        using var bitmap = SKBitmap.Decode(bitmapBytes);

        var info = bitmap.Info;

        using var surface = SKSurface.Create(info);

        var canvas = surface.Canvas;

        canvas.Save();
        
        switch (transformation)
        {
            case TransformationType.RotateCounterClockwise:
                {
                    RotateClockwise(canvas);
                    break;
                }
            case TransformationType.RotateClockwise:
                {
                    RotateCounterClockwise(canvas);
                    break;
                }
            case TransformationType.FlipVertically:
                {
                    FlipVertically(canvas, bitmap.Height);
                    break;
                }
            case TransformationType.FlipHorizontally:
                {
                    FlipHorizontally(canvas, bitmap.Width);
                    break;
                }
            default:
                throw new ArgumentOutOfRangeException(nameof(transformation), transformation, null);
        }

        canvas.Restore();


        return bitmapBytes;
    }

    private void RotateClockwise(SKCanvas canvas)
    {
        canvas.RotateDegrees(NinetyDegrees);
    }

    private void RotateCounterClockwise(SKCanvas canvas)
    {
        canvas.RotateDegrees(-NinetyDegrees);
    }

    private void FlipHorizontally(SKCanvas canvas, int width)
    {
        canvas.Scale(-1, 1);
        canvas.Translate(-width, 0);
    }

    private void FlipVertically(SKCanvas canvas, int height)
    {
        canvas.Scale(1, -1);
        canvas.Translate(0, -height);
    }
}