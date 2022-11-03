using System.Net;
using System.Text;
using AirMastera.Domain.Exceptions;
using AirMastera.Infrastructure.Api.Models;
using Newtonsoft.Json;

namespace AirMastera.Infrastructure.Api.Middleware;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            await HandleExceptionMessageAsync(context, exception);
        }
    }

    private static Task HandleExceptionMessageAsync(HttpContext context, Exception exception)
    {
        var result = string.Empty;
        var code = exception switch
        {
            PhoneException => (int) HttpStatusCode.InternalServerError,
            UriFormatException => (int) HttpStatusCode.InternalServerError,
            NotFoundException => (int) HttpStatusCode.NotFound,
            _ => (int) HttpStatusCode.InternalServerError
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = code;


        if (result == string.Empty)
        {
            result = JsonConvert.SerializeObject(new ExceptionResponse
            {
                Type = exception.GetType().Name,
                StackTrace = BuildStackTrace(exception),
                Data = exception.Data,
                Message = exception.Message
            });
        }

        return context.Response.WriteAsync(result);
    }

    private static string BuildStackTrace(Exception exception, StringBuilder? stringBuilder = null)
    {
        stringBuilder ??= new StringBuilder($"{exception.Message} {exception.StackTrace}");

        if (exception.InnerException == null)
            return stringBuilder.ToString();

        stringBuilder.Append(exception.InnerException);
        return BuildStackTrace(exception.InnerException, stringBuilder);
    }
}