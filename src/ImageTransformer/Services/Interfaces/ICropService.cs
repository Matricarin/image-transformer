using ImageTransformer.Models;

namespace ImageTransformer.Services.Interfaces;

public interface ICropService
{
    Span<byte> Crop(Coordinates coords, Span<byte> bitmapBytes);
}