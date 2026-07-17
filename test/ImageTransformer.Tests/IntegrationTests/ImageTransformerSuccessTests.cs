using System.Net;
using System.Net.Http.Headers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;

namespace ImageTransformer.Tests.IntegrationTests;

public sealed class ImageTransformerSuccessTests
    : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly CancellationTokenSource _cts;
    private readonly WebApplicationFactory<Program> _factory;
    private readonly Uri _hostUri = new("https://localhost:7014");

    public ImageTransformerSuccessTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        _cts = new CancellationTokenSource();
    }

    [Fact]
    public async Task Post_RotateCW_Success()
    {
        var uri = new Uri(_hostUri, "/process/rotate-cw/239,120,39,32");

        var client = _factory.CreateClient();

        var request = new HttpRequestMessage(HttpMethod.Post, uri);

        var rawContent = new ByteArrayContent(ValidImagesData.ValidImagesBytes[ValidTestsCases.RotateCwCase]);

        rawContent.Headers.ContentType = MediaTypeHeaderValue.Parse(TestsConstants.PngMediaType);

        request.Content = rawContent;

        var response = await client.SendAsync(request, _cts.Token);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        using var memory = new MemoryStream();

        await response.Content.CopyToAsync(memory);

        memory.ToArray().Should().BeEqualTo(ResultImagesData.ResultImages[ValidTestsCases.RotateCwCase]);
    }

    [Fact]
    public async Task Post_FlipH_Success()
    {
        var uri = new Uri(_hostUri, "/process/flip-h/86,66,42,38");

        var client = _factory.CreateClient();

        var request = new HttpRequestMessage(HttpMethod.Post, uri);

        var rawContent = new ByteArrayContent(ValidImagesData.ValidImagesBytes[ValidTestsCases.FlipHCase]);

        rawContent.Headers.ContentType = MediaTypeHeaderValue.Parse(TestsConstants.PngMediaType);

        request.Content = rawContent;

        var response = await client.SendAsync(request, _cts.Token);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        using var memory = new MemoryStream();

        await response.Content.CopyToAsync(memory);

        memory.ToArray().Should().BeEqualTo(ResultImagesData.ResultImages[ValidTestsCases.FlipHCase]);
    }
}