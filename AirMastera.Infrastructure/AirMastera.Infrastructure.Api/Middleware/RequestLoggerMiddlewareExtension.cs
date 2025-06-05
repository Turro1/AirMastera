namespace AirMastera.Infrastructure.Api.Middleware;

public static class RequestLoggerMiddlewareExtension
{
    public static IApplicationBuilder UseRequestLoggerHandler(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<RequestLoggerMiddleware>();
    }
}