using ImageTransformer.Models;
using ImageTransformer.Services.Interfaces;
using SkiaSharp;

namespace ImageTransformer.Services;

public sealed class TransformationService : ITransformationService
{
    private const int NinetyDegrees = 90;

    public byte[] Transform(TransformationType transformation, byte[] bitmapBytes)
    {
        using var bitmap = SKBitmap.Decode(bitmapBytes);

        var (targetWidth, targetHeight) = GetTargetSize
        (
            transformation,
            bitmap
        );

        using var targetBitmap = new SKBitmap(targetWidth, targetHeight);

        using var canvas = new SKCanvas(targetBitmap);

        canvas.Save();

        switch (transformation)
        {
            case TransformationType.RotateCounterClockwise:
            {
                RotateClockwise(canvas, bitmap.Height);
                break;
            }
            case TransformationType.RotateClockwise:
            {
                RotateCounterClockwise(canvas, bitmap.Width);
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

        canvas.DrawBitmap(bitmap, 0, 0);

        canvas.Restore();

        using var memoryStream = new MemoryStream();

        targetBitmap.Encode(memoryStream, SKEncodedImageFormat.Png, 100);

        return memoryStream.ToArray();
    }

    private (int targetWidth, int targetHeight) GetTargetSize
    (
        TransformationType transformation,
        SKBitmap bitmap
    )
    {
        switch (transformation)
        {
            case TransformationType.RotateClockwise:
            case TransformationType.RotateCounterClockwise:
                return (bitmap.Height, bitmap.Width);
            case TransformationType.FlipVertically:
            case TransformationType.FlipHorizontally:
                return (bitmap.Width, bitmap.Height);
            default:
                throw new ArgumentOutOfRangeException(nameof(transformation), transformation, null);
        }
    }

    private void RotateClockwise(SKCanvas canvas, int height)
    {
        canvas.RotateDegrees(-NinetyDegrees);
        canvas.Translate(-height, 0);
    }

    private void RotateCounterClockwise(SKCanvas canvas, int width)
    {
        canvas.RotateDegrees(NinetyDegrees);
        canvas.Translate(0, -width);
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