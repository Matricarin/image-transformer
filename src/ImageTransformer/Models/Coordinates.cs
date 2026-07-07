namespace ImageTransformer.Models;

public readonly record struct Coordinates(int X, int Y, int Width, int Height)
{
    public static bool TryParse(string value, out Coordinates result)
    {
        if (string.IsNullOrEmpty(value))
        {
            result = new Coordinates();
            return false;
        }

        var strings = value.Split(',', StringSplitOptions.RemoveEmptyEntries);

        if (strings.Length != 4)
        {
            result = new Coordinates();
            return false;
        }

        var numbers = new int[4];

        for (var i = 0; i < 4; i++)
        {
            if (int.TryParse(strings[i], out var num))
            {
                numbers[i] = num;
            }
            else
            {
                result = new Coordinates();
                return false;
            }
        }

        result = new Coordinates(numbers[0], numbers[1], numbers[2], numbers[3]);
        return true;
    }
}