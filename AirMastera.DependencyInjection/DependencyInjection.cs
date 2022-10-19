using AirMastera.Application.Services.Interfaces;
using AirMastera.Application.Services.Services;
using AirMastera.Infrastructure.Repositories;
using AirMastera.Infrastructure.Repositories.Mappings;
using Microsoft.Extensions.DependencyInjection;

namespace AirMastera.DependencyInjection;

/// <summary>
/// Класс для регистрации сервисов в DI
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Регистрация сервисов
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddPersonServices(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(MappingPersonProfile));

        services.AddTransient<IPersonRepository, PersonRepository>();
        services.AddTransient<IPersonService, PersonService>();

        return services;
    }
}