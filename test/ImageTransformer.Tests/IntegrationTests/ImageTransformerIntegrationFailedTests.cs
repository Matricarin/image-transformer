using System.Net;
using System.Net.Http.Headers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;

namespace ImageTransformer.Tests.IntegrationTests;

public sealed class ImageTransformerIntegrationFailedTests
    : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly CancellationTokenSource _cts;
    private readonly WebApplicationFactory<Program> _factory;
    private readonly Uri _hostUri = new("https://localhost:7014");

    public ImageTransformerIntegrationFailedTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        _cts = new CancellationTokenSource();
    }

    [Theory]
    [ClassData(typeof(InvalidImagesData))]
    public async Task Post_InvalidImage_BadRequest(byte[] imageContent)
    {
        var uri = new Uri(_hostUri, "/process/flip-v/0,0,50,50");

        var client = _factory.CreateClient();

        var request = new HttpRequestMessage(HttpMethod.Post, uri);

        var rawContent = new ByteArrayContent(imageContent);

        rawContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/png");

        request.Content = rawContent;

        var response = await client.SendAsync(request, _cts.Token);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Theory]
    [InlineData("/process/flip-flip/0,0,50,50")]
    [InlineData("/process/rotate-cw/0,0")]
    public async Task Post_InvalidUri_BadRequest(string testUri)
    {
        var uri = new Uri(_hostUri, testUri);

        var client = _factory.CreateClient();

        var request = new HttpRequestMessage(HttpMethod.Post, uri);

        var rawContent = new ByteArrayContent(ValidImagesData.ValidImagesBytes.First());

        rawContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/png");

        request.Content = rawContent;

        var response = await client.SendAsync(request, _cts.Token);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Theory]
    [InlineData("/process/rotate-cw/-500, -500,240,240")]
    [InlineData("/process/flip-h/1000, 1000 ,-200,-200")]
    public async Task Post_InvalidCoordinatesAfterTransformation_NoContent(string testUri)
    {
        var uri = new Uri(_hostUri, testUri);

        var client = _factory.CreateClient();

        var request = new HttpRequestMessage(HttpMethod.Post, uri);

        var rawContent = new ByteArrayContent(ValidImagesData.ValidImagesBytes.First());

        rawContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/png");

        request.Content = rawContent;

        var response = await client.SendAsync(request, _cts.Token);

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}