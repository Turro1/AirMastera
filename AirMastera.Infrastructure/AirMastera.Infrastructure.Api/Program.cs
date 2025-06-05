using AirMastera.Infrastructure.Api.Formatters.Json2;
using AirMastera.Infrastructure.Api.Middleware;
using AirMastera.Infrastructure.Api.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddServices();
builder.Services.AddControllers(options =>
{
    options.InputFormatters.Add(new Json2InputFormatter());
    options.OutputFormatters.Add(new Json2OutputFormatter());
});

var app = builder.Build();
app.UseCors("AllowSpecificOrigin");
app.UseRequestLoggerHandler();
app.UseCustomExceptionHandler();
app.UseTimeLoggerMiddleware();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "API for AirMastera App");
    options.RoutePrefix = string.Empty;
});

app.Run();