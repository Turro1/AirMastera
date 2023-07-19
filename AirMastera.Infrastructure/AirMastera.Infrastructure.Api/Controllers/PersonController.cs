using System.ComponentModel.DataAnnotations;
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
    public async Task<ActionResult> Create(CreatePersonRequest person, CancellationToken cancellationToken)
    {
        return Ok(await _personService.CreatePersonAsync(person, cancellationToken));
    }

    /// <summary>
    /// Обновление персоны и создание авто
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    [HttpPost]
    [Route("UpdatePerson")]
    public async Task<ActionResult> UpdatePerson([FromQuery] [Required] Guid personId, [FromQuery] Guid? carId, [FromBody] UpdatePersonRequest request,
        CancellationToken cancellationToken)
    {
        return Ok(await _personService.UpdatePersonAsync(personId, carId, request, cancellationToken));
    }

    /// <summary>
    /// Обновление авто и создание ремонта
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    [HttpPost]
    [Route("UpdateCar")]
    public async Task UpdateCar(UpdateCarRequest request, CancellationToken cancellationToken)
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

    /// <summary>
    /// Удалить клиента
    /// </summary>
    /// <param name="personId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpDelete]
    [Route("Delete")]
    public async Task Delete([FromQuery] [Required] Guid personId, CancellationToken cancellationToken)
    {
        await _personService.DeletePersonAsync(personId, cancellationToken);
    }
}