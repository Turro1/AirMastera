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
    /// Получение PersonDto
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Person> GetPersonAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Получение Persons
    /// </summary>
    /// <param name="pageNumber"></param>
    /// <param name="pageSize"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IEnumerable<Person>> GetAllPersonsAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);

    Task DeletePersonAsync(Guid id, CancellationToken cancellationToken);
}