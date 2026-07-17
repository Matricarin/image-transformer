using SkiaSharp;

namespace ImageTransformer.Tests.Helpers;

public class ImageComparer : IEqualityComparer<byte[]>
{
    public bool Equals(byte[]? x, byte[]? y)
    {
        if (x is null)
        {
            return false;
        }

        if (y is null)
        {
            return false;
        }

        using var xBitmap = SKBitmap.Decode(x);
        using var yBitmap = SKBitmap.Decode(y);

        var xPixels = xBitmap.Pixels;
        var yPixels = yBitmap.Pixels;

        if (xPixels.Length != yPixels.Length)
        {
            return false;
        }

        return !xPixels.Where((t, i) => t != yPixels[i]).Any();
    }

    public int GetHashCode(byte[] obj)
    {
        using var bitmap = SKBitmap.Decode(obj);

        var pixels = bitmap.Pixels;

        return pixels.GetHashCode();
    }
}