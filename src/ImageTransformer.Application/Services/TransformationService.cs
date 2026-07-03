using ImageTransformer.Application.Interfaces;
using ImageTransformer.Application.Models;
using SkiaSharp;

namespace ImageTransformer.Application.Services;

public sealed class TransformationService : ITransformationService
{
    private const int NinetyDegrees = 90;
    public ReadOnlySpan<byte> Transform(TransformationType transformation, ReadOnlySpan<byte> bitmapBytes)
    {
        //var bitmap = SKBitmap.Decode(bytes); нужно передать span bytes

        //var bitmap = new SKBitmap();

        //var info = bitmap.Info;
        //using (var surface = SKSurface.Create(info))
        //{
        //    var canvas = surface.Canvas;
        //}
    }

    private SKCanvas RotateClockwise(SKCanvas canvas)
    {
        canvas.RotateDegrees(NinetyDegrees);
        return canvas;
    }

    private SKCanvas RotateCounterClockwise(SKCanvas canvas)
    {
        canvas.RotateDegrees(-NinetyDegrees);
        return canvas;
    }

    private SKCanvas FlipHorizontally(SKCanvas canvas, int width)
    {
        canvas.Scale(-1, 1);
        canvas.Translate(-width, 0);
        return canvas;
    }

    private SKCanvas FlipVertically(SKCanvas canvas, int height)
    {
        canvas.Scale(1, -1);
        canvas.Translate(0, -height);
        return canvas;
    }
}