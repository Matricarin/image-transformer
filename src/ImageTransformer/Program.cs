using ImageTransformer.Apis;

var builder = WebApplication.CreateBuilder();

builder.WebHost.UseUrls("http://localhost:8080");

var app = builder.Build();

app.UseHttpsRedirection();

app.MapImageTransformApi();

app.Run();