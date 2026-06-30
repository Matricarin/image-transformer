using ImageTransformer.Apis;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapImageTransformApi();

app.Run();

//  TODO: добавить логирование

//  TODO: добавить трейсы и метрики

//  TODO: добавить документацию