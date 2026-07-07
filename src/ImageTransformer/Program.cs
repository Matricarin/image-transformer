using ImageTransformer.Apis;
using ImageTransformer.Services;

var builder = WebApplication.CreateBuilder();

builder.Services.AddImageTransformServices();

var app = builder.Build();

app.UseHttpsRedirection();

app.MapImageTransformApi();

app.Run();