using System.ComponentModel.DataAnnotations;
using AirMastera.Application.Services.Interfaces;
using AirMastera.Application.Services.Models;
using Microsoft.AspNetCore.Mvc;

namespace AirMastera.Infrastructure.Api.Controllers;

/// <summary>
/// Контроллер Person
/// </summary>
[ApiController]
//[Authorize]
[Route("api/[controller]")]
public class PersonController : ControllerBase
{
    private readonly IPersonService _personService;

    public PersonController(IPersonService personService)
    {
        _personService = personService ?? throw new ArgumentNullException(nameof(personService));
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
    /// Получение клиентов
    /// </summary>
    /// <param name="pageNumber"></param>
    /// <param name="pageSize"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("Get")]
    public async Task<ActionResult> Get(CancellationToken cancellationToken, int pageNumber = 1, int pageSize = 20)
    {
        return Ok(await _personService.GetAllPersonsAsync(pageNumber, pageSize, cancellationToken));
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