using ImageTransformer.Models;

namespace ImageTransformer.Services.Interfaces;

public interface ITransformationService
{
    ReadOnlySpan<byte> Transform(TransformationType transformation, ReadOnlySpan<byte> bitmapBytes);
}