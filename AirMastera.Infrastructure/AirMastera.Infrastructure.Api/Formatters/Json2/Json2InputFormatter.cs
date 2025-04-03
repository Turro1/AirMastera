using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace AirMastera.Infrastructure.Api.Formatters.Json2;

public class Json2InputFormatter : TextInputFormatter
{
    public Json2InputFormatter()
    {
        SupportedMediaTypes.Add("application/json2");
        SupportedEncodings.Add(Encoding.UTF8);
    }

    public override async Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context, Encoding encoding)
    {
        var request = context.HttpContext.Request;

        using var reader = new StreamReader(request.Body, encoding);
        var json2 = await reader.ReadToEndAsync();

        if (json2.StartsWith("json2:"))
        {
            json2 = json2.Substring(6);
        }

        var obj = JsonSerializer.Deserialize(json2, context.ModelType, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        return await InputFormatterResult.SuccessAsync(obj);
    }
}