using ImageTransformer.Models;

namespace ImageTransformer.Services.Interfaces;

public interface ITransformationService
{
    byte[] Transform(TransformationType transformation, MemoryStream stream);
}