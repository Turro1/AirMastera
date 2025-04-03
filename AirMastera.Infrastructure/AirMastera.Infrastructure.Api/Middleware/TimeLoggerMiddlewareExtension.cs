namespace AirMastera.Infrastructure.Api.Middleware;

public static class TimeLoggerMiddlewareExtension
{
    public static IApplicationBuilder UseTimeLoggerMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<TimeLoggerMiddleware>();
    }
}