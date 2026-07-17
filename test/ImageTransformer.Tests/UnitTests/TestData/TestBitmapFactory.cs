using SkiaSharp;

namespace ImageTransformer.Tests.UnitTests.TestData;

public static class TestBitmapFactory
{
    public static SKBitmap CreateRedBlueSquare(TestTwoColorsSquare squareData)
    {
        var source = new SKBitmap(squareData.Width, squareData.Height, SKColorType.Bgra8888, SKAlphaType.Opaque);

        using var canvas = new SKCanvas(source);

        canvas.Save();

        canvas.DrawPoint(0, 0, SKColor.Parse(squareData.FirstColorHexString));
        canvas.DrawPoint(0, 1, SKColor.Parse(squareData.FirstColorHexString));
        canvas.DrawPoint(1, 0, SKColor.Parse(squareData.SecondColorHexString));
        canvas.DrawPoint(1, 1, SKColor.Parse(squareData.SecondColorHexString));

        canvas.Restore();

        return source;
    }
}