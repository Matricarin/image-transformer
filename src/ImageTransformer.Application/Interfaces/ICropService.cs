using ImageTransformer.Application.Models;

namespace ImageTransformer.Application.Interfaces;

public interface ICropService
{
    ReadOnlySpan<byte> Crop(Coordinates coords, ReadOnlySpan<byte> bitmapBytes);
}