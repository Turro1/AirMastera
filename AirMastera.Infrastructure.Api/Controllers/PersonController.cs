using AirMastera.Application.Services.Interfaces;
using AirMastera.Application.Services.Models;
using AirMastera.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AirMastera.Infrastructure.Api.Controllers;

/// <summary>
/// Контроллер для получения pong
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class PersonController : ControllerBase
{
    private readonly IPersonService _personService;

    public PersonController(IPersonService personService)
    {
        _personService = personService;
    }

    /// <summary>
    /// Создание персоны
    /// </summary>
    /// <returns>pong</returns>
    [HttpPost]
    public  async Task<Person> Create(Person person, CancellationToken cancellationToken)
    {
        await _personService.CreatePersonAsync(person, cancellationToken);
        return person;
    }

    /// <summary>
    /// Получение персоны
    /// </summary>
    /// <returns>pong</returns>
    [HttpGet]
    public async Task<PersonDto> Get(Guid id, CancellationToken cancellationToken)
    {
        var res = await _personService.GetPersonDtoAsync(id, cancellationToken);

        return res;
    }
}