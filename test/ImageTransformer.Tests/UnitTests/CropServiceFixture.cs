using ImageTransformer.Models;
using ImageTransformer.Services;
using ImageTransformer.Tests.UnitTests.TestData;
using SkiaSharp;

namespace ImageTransformer.Tests.UnitTests;

public sealed class CropServiceFixture
{
    [Fact]
    public void Crop_Success()
    {
        var testData = new TestTwoColorsSquare(Constants.RedHex, Constants.BlueHex, 2, 2);
        using var source = TestBitmapFactory.CreateRedBlueSquare(testData);

        using var memory = new MemoryStream();

        source.Encode(memory, SKEncodedImageFormat.Png, 1);

        var service = new CropService();

        var cropped = service.Crop
        (
            new Coordinates(0, 0, 1, 1),
            memory.ToArray()
        );

        using var croppedBitmap = SKBitmap.Decode(cropped);

        var pixel = croppedBitmap.GetPixel(0, 0);

        var expectedPixel = new SKColor(255, 0, 0);

        Assert.True(pixel.Red == expectedPixel.Red);
        Assert.True(pixel.Blue == expectedPixel.Blue);
        Assert.True(pixel.Green == expectedPixel.Green);
    }
}