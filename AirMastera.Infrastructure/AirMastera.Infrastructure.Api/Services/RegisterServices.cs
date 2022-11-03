using System.Reflection;
using AirMastera.DependencyInjection;
using AirMastera.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AirMastera.Infrastructure.Api.Services;

public static class RegisterServices
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddControllers();

        services.AddSwaggerGen(options =>
        {
            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        });

        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .AddJsonFile("appsettings.Development.json", optional: true);
        var config = builder.Build();

        services.AddDbContext<AirMasteraDbContext>(options =>
        {
            var connectionString = config.GetConnectionString("PostgresConnection");
            options.UseNpgsql(connectionString);
        });
        services.AddPersonServices();
        return services;
    }
}