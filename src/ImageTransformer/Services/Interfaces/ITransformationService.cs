using ImageTransformer.Models;

namespace ImageTransformer.Services.Interfaces;

public interface ITransformationService
{
    Span<byte> Transform(TransformationType transformation, MemoryStream stream);
}