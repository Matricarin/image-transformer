using System.Collections;

namespace ImageTransformer.Tests.IntegrationTests;

public sealed class InvalidImagesData : IEnumerable<object[]>
{
    private static readonly string _folderPath =
        Path.Combine(Environment.CurrentDirectory, "TestImages", "Failed");

    public static readonly object[][] InvalidImages =
    [
        [File.ReadAllBytes(Path.Combine(_folderPath, "image1-3000-1000.png"))],
        [File.ReadAllBytes(Path.Combine(_folderPath, "image1-537KB.png"))],
        [File.ReadAllBytes(Path.Combine(_folderPath, "image1-bmp.bmp"))],
        [File.ReadAllBytes(Path.Combine(_folderPath, "image2-122KB.png"))]
    ];

    public IEnumerator<object[]> GetEnumerator()
    {
        return ((IEnumerable<object[]>)InvalidImages)
            .GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}