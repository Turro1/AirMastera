using AirMastera.Infrastructure.Api.Middleware;
using AirMastera.Infrastructure.Api.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddServices();
var app = builder.Build();

app.UseCustomExceptionHandler();
app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("swagger/v1/swagger.json", "API for AirMastera App");
    options.RoutePrefix = string.Empty;
});

app.Run();