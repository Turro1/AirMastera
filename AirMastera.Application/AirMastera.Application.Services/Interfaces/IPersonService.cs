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
    Task<PersonDto> CreatePersonAsync(CreatePersonRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Обновление Person
    /// </summary>
    /// <param name="personId"></param>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<PersonDto> UpdatePersonAsync(Guid personId, Guid? carId, UpdatePersonRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Обновление Person
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task UpdateCarAsync(UpdateCarRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Получение PersonDto
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Person> GetPersonAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Получение Car
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Car> GetCarAsync(Guid id, CancellationToken cancellationToken);

    Task DeletePersonAsync(Guid id, CancellationToken cancellationToken);
}