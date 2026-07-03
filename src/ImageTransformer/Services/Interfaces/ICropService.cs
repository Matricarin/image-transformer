using ImageTransformer.Models;

namespace ImageTransformer.Services.Interfaces;

public interface ICropService
{
    ReadOnlySpan<byte> Crop(Coordinates coords, ReadOnlySpan<byte> bitmapBytes);
}