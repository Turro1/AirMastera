using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace AirMastera.Infrastructure.Api.Formatters.Json2;

public class Json2OutputFormatter : TextOutputFormatter
{
    public Json2OutputFormatter()
    {
        SupportedMediaTypes.Add("application/json2");
        SupportedEncodings.Add(Encoding.UTF8);
    }

    public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
    {
        var response = context.HttpContext.Response;
        response.ContentType = "application/json2";

        var json = JsonSerializer.Serialize(context.Object, new JsonSerializerOptions
        {
            WriteIndented = false,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });
        var json2 = $"json2:{json}";

        await response.WriteAsync(json2, selectedEncoding);
    }
}