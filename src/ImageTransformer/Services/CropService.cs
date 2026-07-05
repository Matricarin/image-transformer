using ImageTransformer.Models;
using ImageTransformer.Services.Interfaces;
using SkiaSharp;

namespace ImageTransformer.Services;

public sealed class CropService : ICropService
{
    public Span<byte> Crop(Coordinates coords, Span<byte> bitmapBytes)
    {
        using var sourceBitmap = SKBitmap.Decode(bitmapBytes);

        var destRect = new SKRect(0, 0, coords.Width, coords.Height);

        using var destBitmap = new SKBitmap(coords.Width, coords.Height);

        var sourceRect = new SKRect
        (
            coords.X,
            coords.Y,
            coords.X - coords.Width,
            coords.Y - coords.Height
        );

        using (var canvas = new SKCanvas(destBitmap))
        {
            canvas.DrawBitmap(sourceBitmap, sourceRect, destRect);
        }

        return destBitmap.GetPixelSpan();
    }
}