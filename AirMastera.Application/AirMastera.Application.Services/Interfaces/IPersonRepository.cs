﻿using AirMastera.Application.Services.Models;
using AirMastera.Domain.Entities;

namespace AirMastera.Application.Services.Interfaces;

public interface IPersonRepository
{
    /// <summary>
    /// Создание Person
    /// </summary>
    /// <param name="person"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<PersonDto> CreatePersonAsync(Person person, CancellationToken cancellationToken);

    /// <summary>
    /// Обновление Person
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<PersonDto> UpdatePersonAsync(Person request, CancellationToken cancellationToken);

    /// <summary>
    /// Получение PersonDto
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<PersonDto> GetPersonDtoAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Получение Person
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Person> GetPersonAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Получение Pesrons
    /// </summary>
    /// <param name="pageNumber"></param>
    /// <param name="pageSize"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IEnumerable<Person>> GetAllPersonsAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);

    /// <summary>
    /// Удаление Person
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task DeletePersonAsync(Guid id, CancellationToken cancellationToken);
}