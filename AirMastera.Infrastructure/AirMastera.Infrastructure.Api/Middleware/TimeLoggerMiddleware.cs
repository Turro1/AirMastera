namespace AirMastera.Infrastructure.Api.Middleware;

public class TimeLoggerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<TimeLoggerMiddleware> _logger;

    public TimeLoggerMiddleware(
        RequestDelegate next,
        ILogger<TimeLoggerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        _logger.LogDebug($"Request start: {DateTime.Now}");

        await _next.Invoke(context);

        _logger.LogDebug($"Request finish: {DateTime.Now}");
    }
}