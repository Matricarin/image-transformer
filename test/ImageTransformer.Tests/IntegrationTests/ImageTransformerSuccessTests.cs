using System.Net;
using System.Net.Http.Headers;
using FluentAssertions;
using ImageTransformer.Tests.Helpers;
using Microsoft.AspNetCore.Mvc.Testing;

namespace ImageTransformer.Tests.IntegrationTests;

public sealed class ImageTransformerSuccessTests
    : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly CancellationTokenSource _cts;
    private readonly WebApplicationFactory<Program> _factory;
    private readonly Uri _hostUri = new("https://localhost:7014");
    private readonly ImageComparer _imageComparer;

    public ImageTransformerSuccessTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        _cts = new CancellationTokenSource();
        _imageComparer = new ImageComparer();
    }

    [Fact]
    public async Task Post_RotateCW_Success()
    {
        var uri = new Uri(_hostUri,
            $"/process/{ValidTestsCases.RotateCwCase}/104,108,128,152");

        var client = _factory.CreateClient();

        var request = new HttpRequestMessage(HttpMethod.Post, uri);

        var rawContent = new ByteArrayContent
        (
            ValidImagesData.ValidImagesBytes[ValidTestsCases.RotateCwCase]
        );

        rawContent.Headers.ContentType = MediaTypeHeaderValue.Parse(TestsConstants.PngMediaType);

        request.Content = rawContent;

        var response = await client.SendAsync(request, _cts.Token);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        using var memory = new MemoryStream();

        await response.Content.CopyToAsync(memory);

        Assert.True(_imageComparer.Equals(memory.ToArray(),
            ResultImagesData.ResultImages[ValidTestsCases.RotateCwCase]));
    }

    [Fact]
    public async Task Post_FlipV_Success()
    {
        var uri = new Uri(_hostUri,
            $"/process/{ValidTestsCases.FlipVCase}/86,66,42,38");

        var client = _factory.CreateClient();

        var request = new HttpRequestMessage(HttpMethod.Post, uri);

        var rawContent = new ByteArrayContent
        (
            ValidImagesData.ValidImagesBytes[ValidTestsCases.FlipVCase]
        );

        rawContent.Headers.ContentType = MediaTypeHeaderValue.Parse(TestsConstants.PngMediaType);

        request.Content = rawContent;

        var response = await client.SendAsync(request, _cts.Token);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        using var memory = new MemoryStream();

        await response.Content.CopyToAsync(memory);

        Assert.True(_imageComparer.Equals(memory.ToArray(),
            ResultImagesData.ResultImages[ValidTestsCases.FlipVCase]));
    }
}