namespace AirMastera.Infrastructure.Api.Middleware;

public class RequestLoggerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggerMiddleware> _logger;

    public RequestLoggerMiddleware(RequestDelegate next,
        ILogger<RequestLoggerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        context.Request.EnableBuffering();
        using var requestReader = new StreamReader(context.Request.Body);
        var requestBody = await requestReader.ReadToEndAsync();
        _logger.LogDebug($"Request body: {requestBody}");
        context.Request.Body.Position = 0;

        var originalResponseBody = context.Response.Body;
        await using var buffer = new MemoryStream();
        context.Response.Body = buffer;

        await _next(context);

        context.Response.Body.Seek(0, SeekOrigin.Begin);
        using var responseReader = new StreamReader(context.Response.Body);
        var  responseBody = await  responseReader.ReadToEndAsync();
        _logger.LogDebug($"Response body: { responseBody}");
        context.Response.Body.Seek(0, SeekOrigin.Begin);
        await context.Response.Body.CopyToAsync(originalResponseBody);
        context.Response.Body = originalResponseBody;
    }
}