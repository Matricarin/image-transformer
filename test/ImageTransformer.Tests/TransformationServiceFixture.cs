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

        using var memoryStream = new MemoryStream();

        source.Encode(memoryStream, SKEncodedImageFormat.Png, 1);

        var transformedBitmap = _service.Transform(transformation, memoryStream);


    }

    public void RotateCounterClockwise_Success()
    {
        {
            var transformation = TransformationType.RotateCounterClockwise;

            using var source = TestBitmapFactory.CreateRedBlueSquare();
        }
    }

    public void FlipVertically_Success()
    {
        {
            var transformation = TransformationType.FlipVertically;

            using var source = TestBitmapFactory.CreateRedBlueSquare();
        }
    }

    public void FlipHorizontally_Success()
    {
        {
            var transformation = TransformationType.FlipHorizontally;

            using var source = TestBitmapFactory.CreateRedBlueSquare();
        }
    }
}