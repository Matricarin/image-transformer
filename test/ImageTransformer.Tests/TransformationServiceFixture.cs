using ImageTransformer.Models;
using ImageTransformer.Services;
using ImageTransformer.Services.Interfaces;
using ImageTransformer.Tests.TestData;
using SkiaSharp;

namespace ImageTransformer.Tests;

public sealed class TransformationServiceFixture
{
    private readonly ITransformationService _service;
    private readonly TestTwoColorsSquare _testSquare;

    public TransformationServiceFixture()
    {
        _service = new TransformationService();
        _testSquare = new TestTwoColorsSquare(Constants.RedHex, Constants.BlueHex, 2, 2);
    }

    [Fact]
    public void RotateClockwise_Success()
    {
        var transformation = TransformationType.RotateClockwise;

        using var source = TestBitmapFactory.CreateRedBlueSquare(_testSquare);

        var expectedPixel = source.GetPixel(0, 0);

        using var memoryStream = new MemoryStream();

        source.Encode(memoryStream, SKEncodedImageFormat.Png, 1);

        var transformedBytes = _service.Transform
        (
            transformation,
            memoryStream.ToArray()
        );

        using var transformedBitmap = SKBitmap.Decode(transformedBytes);

        var pixel = transformedBitmap.GetPixel(1, 0);

        Assert.True(pixel == expectedPixel);
    }

    [Fact]
    public void RotateCounterClockwise_Success()
    {
        var transformation = TransformationType.RotateCounterClockwise;

        using var source = TestBitmapFactory.CreateRedBlueSquare(_testSquare);

        var expectedPixel = source.GetPixel(1, 0);

        using var memoryStream = new MemoryStream();

        source.Encode(memoryStream, SKEncodedImageFormat.Png, 1);

        var transformedBytes = _service.Transform(transformation, memoryStream.ToArray());

        using var transformedBitmap = SKBitmap.Decode(transformedBytes);

        var pixel = transformedBitmap.GetPixel(0, 0);

        Assert.True(pixel == expectedPixel);
    }

    [Fact]
    public void FlipVertically_Success()
    {
        var transformation = TransformationType.FlipVertically;

        using var source = TestBitmapFactory.CreateRedBlueSquare(_testSquare);

        var expectedPixel = source.GetPixel(0, 0);

        using var memoryStream = new MemoryStream();

        source.Encode(memoryStream, SKEncodedImageFormat.Png, 1);

        var transformedBytes = _service.Transform(transformation, memoryStream.ToArray());

        using var transformedBitmap = SKBitmap.Decode(transformedBytes);

        var pixel = transformedBitmap.GetPixel(0, 1);

        Assert.True(pixel == expectedPixel);
    }

    [Fact]
    public void FlipHorizontally_Success()
    {
        var transformation = TransformationType.RotateClockwise;

        using var source = TestBitmapFactory.CreateRedBlueSquare(_testSquare);

        var expectedPixel = source.GetPixel(0, 0);

        using var memoryStream = new MemoryStream();

        source.Encode(memoryStream, SKEncodedImageFormat.Png, 1);

        var transformedBytes = _service.Transform(transformation, memoryStream.ToArray());

        using var transformedBitmap = SKBitmap.Decode(transformedBytes);

        var pixel = transformedBitmap.GetPixel(1, 0);

        Assert.True(pixel == expectedPixel);
    }
}