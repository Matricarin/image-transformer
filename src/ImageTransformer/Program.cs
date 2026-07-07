using ImageTransformer.Apis;

var builder = WebApplication.CreateBuilder();

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenLocalhost(8080);
});

var app = builder.Build();

app.UseHttpsRedirection();

app.MapImageTransformApi();

app.Run();