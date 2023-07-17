using AirMastera.Application.Services.Interfaces;
using AirMastera.Application.Services.Models;
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
    /// <param name="person"></param>
    /// <param name="cancellationToken"></param>
    [HttpPost]
    [Route("Create")]
    public async Task Create(CreatePersonRequest person, CancellationToken cancellationToken)
    {
        await _personService.CreatePersonAsync(person, cancellationToken);
    }

    /// <summary>
    /// Обновление персоны и создание авто
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    [HttpPost]
    [Route("SaveOrUpdateCar")]
    public async Task SaveOrUpdateCar(UpdatePersonRequest request, CancellationToken cancellationToken)
    {
        await _personService.UpdatePersonAsync(request, cancellationToken);
    }

    /// <summary>
    /// Обновление авто и создание ремонта
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    [HttpPost]
    [Route("SaveOrUpdateRepair")]
    public async Task SaveOrUpdateRepair(UpdateCarRequest request, CancellationToken cancellationToken)
    {
        await _personService.UpdateCarAsync(request, cancellationToken);
    }

    /// <summary>
    /// Получение персоны
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("Get")]
    public async Task<ActionResult> Get(Guid id, CancellationToken cancellationToken)
    {
        return Ok(await _personService.GetPersonAsync(id, cancellationToken));
    }
}