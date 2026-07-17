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
    private readonly Uri HostUri = new("https://localhost:7014");

    public ImageTransformerIntegrationFailedTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        _cts = new CancellationTokenSource();
    }

    [Theory]
    [ClassData(typeof(InvalidImagesData))]
    public async Task Post_InvalidImage_BadRequest(byte[] imageContent)
    {
        var uri = new Uri(HostUri, "/process/flip-v/0,0,50,50");

        var client = _factory.CreateClient();

        var request = new HttpRequestMessage(HttpMethod.Post, uri);

        var rawContent = new ByteArrayContent(imageContent);

        rawContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/png");

        request.Content = rawContent;

        var response = await client.SendAsync(request, _cts.Token);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}