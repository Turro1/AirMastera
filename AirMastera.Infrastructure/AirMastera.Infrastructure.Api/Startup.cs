using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace AirMastera.Infrastructure.Api;

public class Startup
{
    public void Configure(IApiVersionDescriptionProvider provider, SwaggerUIOptions config)
    {
        foreach (var description in provider.ApiVersionDescriptions)
        {
            config.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());

            config.RoutePrefix = String.Empty;
        }
    }
}