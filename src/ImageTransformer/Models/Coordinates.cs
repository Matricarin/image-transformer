namespace ImageTransformer.Models;

public readonly record struct Coordinates(int X, int Y, int Width, int Height)
{
    public static bool TryParse(string value, out Coordinates result)
    {
        throw new NotImplementedException();
    }
}