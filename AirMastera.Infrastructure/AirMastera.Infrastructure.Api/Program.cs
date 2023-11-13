using AirMastera.Infrastructure.Api;
using AirMastera.Infrastructure.Api.Middleware;
using AirMastera.Infrastructure.Api.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddServices();
var app = builder.Build();
app.UseCors("AllowSpecificOrigin");

var startup = new Startup();

app.UseCustomExceptionHandler();
app.MapControllers();
app.UseSwagger();

app.UseAuthentication();
app.UseAuthorization();

app.UseSwaggerUI(options =>
{
    //startup.Configure(IApiDescriptionProvider,options);
    options.SwaggerEndpoint("swagger/v1/swagger.json", "API for AirMastera App");
    options.RoutePrefix = string.Empty;
});

app.Run();