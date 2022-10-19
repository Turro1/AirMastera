using System.Collections;

namespace AirMastera.Infrastructure.Api.Models;

public class ExceptionResponse

{
    public string? Type { get; set; }
    public string? StackTrace { get; set; }
    public IDictionary? Data { get; set; }
    public string? Message { get; set; }
}