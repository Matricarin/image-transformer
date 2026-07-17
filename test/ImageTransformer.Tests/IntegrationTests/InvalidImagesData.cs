using System.Collections;

namespace ImageTransformer.Tests.IntegrationTests;

public class InvalidImagesData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        var path = Environment.CurrentDirectory + "/TestImages/Failed/";


        yield return [File.ReadAllBytes(Path.Combine(path, "image1-3000-1000.png"))];

        yield return [File.ReadAllBytes(Path.Combine(path, "image1-537KB.png"))];

        yield return [File.ReadAllBytes(Path.Combine(path, "image1-bmp.bmp"))];

        yield return [File.ReadAllBytes(Path.Combine(path, "image2-122KB.png"))];
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}