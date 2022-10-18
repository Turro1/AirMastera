using AirMastera.Application.Services.Models;
using AirMastera.Domain;

namespace AirMastera.Application.Services.Interfaces;

public interface IPersonService
{
    /// <summary>
    /// Создание Person
    /// </summary>
    /// <param name="person"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task CreatePersonAsync(Person person, CancellationToken cancellationToken);

    /// <summary>
    /// Обновление Person
    /// </summary>
    /// <param name="person"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task UpdatePersonAsync(Person person, CancellationToken cancellationToken);

    /// <summary>
    /// Получение PersonDto
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<PersonDto> GetPersonDtoAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Удаление PersonDto
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<PersonDto> DeletePersonDtoAsync(Guid id, CancellationToken cancellationToken);
}