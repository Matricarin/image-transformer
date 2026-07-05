using ImageTransformer.Models;
using SkiaSharp;

namespace ImageTransformer.Services.Interfaces;

public interface ICropService
{
    byte[] Crop(Coordinates coords, byte[] bitmapBytes);
}