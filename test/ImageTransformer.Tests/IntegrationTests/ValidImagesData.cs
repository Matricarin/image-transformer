using System.Collections;

namespace ImageTransformer.Tests.IntegrationTests;

public sealed class ValidImagesData : IEnumerable<object[]>
{
    private static readonly string _folderPath =
        Path.Combine(Environment.CurrentDirectory, "TestImages", "Success");

    public static Dictionary<string, byte[]> ValidImagesBytes = new()
    {
        { ValidTestsCases.RotateCwCase,
            File.ReadAllBytes(Path.Combine(_folderPath, "image1-500-500.png")) },

        { ValidTestsCases.FlipVCase,
            File.ReadAllBytes(Path.Combine(_folderPath, "image1-250-250.png")) }
    };

    public static readonly object[][] ValidImages =
    [
        [File.ReadAllBytes(Path.Combine(_folderPath, "image1-500-500.png"))],
        [File.ReadAllBytes(Path.Combine(_folderPath, "image1-250-250.png"))]
    ];

    public IEnumerator<object[]> GetEnumerator()
    {
        return ValidImages.Select(validImage => (object[])[validImage])
            .GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}