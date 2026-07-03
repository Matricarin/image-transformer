using ImageTransformer.Apis;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.UseHttpsRedirection();

app.MapImageTransformApi();

app.Run();