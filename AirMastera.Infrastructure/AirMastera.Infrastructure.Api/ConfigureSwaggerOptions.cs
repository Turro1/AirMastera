﻿using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace AirMastera.Infrastructure.Api;

public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
{
    private readonly IApiVersionDescriptionProvider _provider;

    public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
    {
        _provider = provider ?? throw new ArgumentNullException(nameof(provider));
    }

    public void Configure(SwaggerGenOptions options)
    {
        foreach (var description in _provider.ApiVersionDescriptions)
        {
            var apiVersion = description.ApiVersion.ToString();
            options.SwaggerDoc(description.GroupName, new OpenApiInfo()
            {
                Version = apiVersion,
                Title = $"AirMastera API {apiVersion}",
                Description = "A sipmle ASP NET Core Web API. Proffesional way",
                TermsOfService = new Uri("https://www.youtube.com"),
                Contact = new OpenApiContact()
                {
                    Name = "Turro",
                    Email = string.Empty,
                    Url = new Uri("https://t.me")
                },
                License = new OpenApiLicense()
                {
                    Name = "Turro",
                    Url = new Uri("https://t.me")
                }
            });

            options.AddSecurityDefinition($"AuthToken {apiVersion}", new OpenApiSecurityScheme()
            {
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "bearer",
                Name = "Authorization",
                Description = "Authorization token"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference()
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = $"AuthToken {apiVersion}"
                        }
                    },
                    new string[] { }
                }
            });

            options.CustomOperationIds(apiDescription => apiDescription.TryGetMethodInfo(out MethodInfo methodInfo) ? methodInfo.Name : null);
        }
    }
}