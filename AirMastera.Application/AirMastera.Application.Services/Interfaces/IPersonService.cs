using AirMastera.Application.Services.Models;
using AirMastera.Domain.Entities;

namespace AirMastera.Application.Services.Interfaces;

public interface IPersonService
{
    /// <summary>
    /// Создание Person
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task CreatePersonAsync(CreatePersonRequest request, CancellationToken cancellationToken);

    Task<CarDto> SaveWorkExperienceAsync(Guid id, SaveCarRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Обновление Person
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task UpdatePersonAsync(UpdatePersonRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Получение PersonDto
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Person> GetPersonAsync(Guid id, CancellationToken cancellationToken);

    Task<CarDto> SaveCarAsync(Guid id, SaveCarRequest car, CancellationToken cancellationToken);

    Task DeletePersonAsync(Guid id, CancellationToken cancellationToken);
}