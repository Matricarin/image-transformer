using ImageTransformer.Models;
using ImageTransformer.Services.Interfaces;
using SkiaSharp;

namespace ImageTransformer.Services;

public sealed class CropService : ICropService
{
    public ReadOnlySpan<byte> Crop(Coordinates coords, ReadOnlySpan<byte> bitmapBytes)
    {
        var sourceBitmap = SKBitmap.Decode(bitmapBytes);

        var destRect = new SKRect(0, 0, coords.Width, coords.Height);

        var destBitmap = new SKBitmap(coords.Width, coords.Height);

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

        return destBitmap.Bytes;
    }
}