using ImageTransformer.Application.Models;

namespace ImageTransformer.Application.Interfaces;

public interface ITransformationService
{
    void Transform(TransformationType transformation);
}