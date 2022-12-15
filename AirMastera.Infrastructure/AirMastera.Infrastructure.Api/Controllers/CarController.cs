using AirMastera.Application.Services.Interfaces;
using AirMastera.Application.Services.Models;
using Microsoft.AspNetCore.Mvc;

namespace AirMastera.Infrastructure.Api.Controllers;

/// <summary>
/// Контроллер Car
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class CarController : ControllerBase
{
    private readonly IPersonService _personService;

    public CarController(IPersonService personService)
    {
        _personService = personService;
    }

    /// <summary>
    /// Создание Авто для Person
    /// </summary>
    /// <returns>pong</returns>
    [HttpPost]
    public async Task<SaveCarRequest> SaveCar(Guid id, SaveCarRequest car, CancellationToken cancellationToken)
    {
        await _personService.SaveCarAsync(id, car, cancellationToken);
        return car;
    }
}