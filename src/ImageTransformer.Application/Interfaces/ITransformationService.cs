using ImageTransformer.Application.Models;

namespace ImageTransformer.Application.Interfaces;

public interface ITransformationService
{
    ReadOnlySpan<byte> Transform(TransformationType transformation, ReadOnlySpan<byte> bitmapBytes);
}