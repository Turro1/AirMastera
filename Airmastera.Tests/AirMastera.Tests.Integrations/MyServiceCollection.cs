using AirMastera.DependencyInjection;
using AirMastera.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Integrations;

/// <summary>
/// Класс для создания SeriveceProvider
/// </summary>
public class MyServiceCollection
{
    /// <summary>
    /// Создание ServiceProvider
    /// </summary>
    /// <returns></returns>
    public IServiceProvider CreateServiceProvider()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddDbContext<AirMasteraDbContext>(options =>
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.local.json", optional: true);

            var config = builder.Build();

            var connectionString = config.GetConnectionString("PostgresConnection");
            if (connectionString != null) options.UseNpgsql(connectionString);
        });
        serviceCollection.AddPersonServices();

        return serviceCollection.BuildServiceProvider();
    }
}