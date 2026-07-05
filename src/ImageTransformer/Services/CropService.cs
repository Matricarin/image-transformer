using ImageTransformer.Models;
using ImageTransformer.Services.Interfaces;
using SkiaSharp;

namespace ImageTransformer.Services;

public sealed class CropService : ICropService
{
    public byte[] Crop(Coordinates coords, byte[] bitmapBytes)
    {
        using var sourceBitmap = SKBitmap.Decode(bitmapBytes);

        var destRect = new SKRect(0, 0, coords.Width, coords.Height);

        using var destBitmap = new SKBitmap(coords.Width, coords.Height);

        var sourceRect = new SKRect
        (
            coords.X,
            coords.Y,
            coords.X + coords.Width,
            coords.Y + coords.Height
        );

        using (var canvas = new SKCanvas(destBitmap))
        {
            canvas.DrawBitmap(sourceBitmap, sourceRect, destRect);
        }

        using var memoryStream = new MemoryStream();

        destBitmap.Encode(memoryStream, SKEncodedImageFormat.Png, 1);

        return memoryStream.ToArray();
    }
}