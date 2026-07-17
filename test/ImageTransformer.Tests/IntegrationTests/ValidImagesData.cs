using System.Collections;

namespace ImageTransformer.Tests.IntegrationTests;

public sealed class ValidImagesData : IEnumerable<object[]>
{
    private static readonly string _folderPath = Environment.CurrentDirectory + "/TestImages/Success/";

    public static readonly object[][] ValidImages =
    [
        [File.ReadAllBytes(Path.Combine(_folderPath, "image1-500-500.png"))],
        [File.ReadAllBytes(Path.Combine(_folderPath, "image1-250-250.png"))]
    ];

    public IEnumerator<object[]> GetEnumerator()
    {
        return ValidImages.Select(validImage => (object[])[validImage]).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public static List<byte[]> ValidImagesBytes =>
        new()
        {
            File.ReadAllBytes(Path.Combine(_folderPath, "image1-500-500.png")),
            File.ReadAllBytes(Path.Combine(_folderPath, "image1-250-250.png"))
        };
}