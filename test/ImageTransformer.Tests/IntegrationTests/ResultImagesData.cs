using System.Collections;

namespace ImageTransformer.Tests.IntegrationTests;

public sealed class ResultImagesData : IEnumerable<object[]>
{
    private static readonly string _folderPath =
        Path.Combine(Environment.CurrentDirectory, "ResultImages");

    public static Dictionary<string, byte[]> ResultImages = new()
    {
        { ValidTestsCases.RotateCwCase, File.ReadAllBytes(Path.Combine(_folderPath, "image1-239-120-39-32.png")) },
        { ValidTestsCases.FlipHCase, File.ReadAllBytes(Path.Combine(_folderPath, "image1-86-66-42-38.png")) }
    };

    public IEnumerator<object[]> GetEnumerator()
    {
        return ResultImages.Values.Select(image => (object[])[image]).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}