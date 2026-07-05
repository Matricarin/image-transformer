using ImageTransformer.Models;
using ImageTransformer.Services;
using SkiaSharp;

namespace ImageTransformer.Tests;

public sealed class CropServiceFixture
{
    private const string RedHex = "#FF0000";
    private const string BlueHex = "#0000FF";

    [Fact]
    public void Crop_Success()
    {
        using var source = new SKBitmap(2, 2, SKColorType.Bgra8888, SKAlphaType.Opaque);

        var canvas = new SKCanvas(source);

        canvas.Save();

        canvas.DrawPoint(0, 0, SKColor.Parse(RedHex));
        canvas.DrawPoint(0, 1, SKColor.Parse(RedHex));
        canvas.DrawPoint(1, 0, SKColor.Parse(BlueHex));
        canvas.DrawPoint(1, 1, SKColor.Parse(BlueHex));

        canvas.Restore();

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