using SkiaSharp;

namespace ImageTransformer.Tests.UnitTests.TestData;

public sealed class TestTwoColorsSquare
{
    public string FirstColorHexString { get; }
    public string SecondColorHexString { get; }
    public int Width { get; }
    public int Height { get; }
    public SKColor FirstColor => SKColor.Parse(FirstColorHexString);
    public SKColor SecondColor => SKColor.Parse(SecondColorHexString);

    public TestTwoColorsSquare
    (
        string firstColorHexString,
        string secondColorHexString,
        int width,
        int height
    )
    {
        FirstColorHexString = firstColorHexString;
        SecondColorHexString = secondColorHexString;
        Width = width;
        Height = height;
    }
}