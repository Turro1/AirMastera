using System.ComponentModel.DataAnnotations;
using AirMastera.Application.Services.Interfaces;
using AirMastera.Application.Services.Models;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AirMastera.Infrastructure.Api.Controllers;

/// <summary>
/// Контроллер для получения pong
/// </summary>
[ApiController]
[Authorize]
[Route("api/[controller]")]
public class CarController : ControllerBase
{
    private readonly ICarService _carService;

    public CarController(ICarService carService)
    {
        _carService = carService ?? throw new ArgumentNullException(nameof(carService));
    }

    /// <summary>
    /// Получение токена авторизации
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route("GetToken")]
    public async Task<ActionResult> GetToken()
    {
        var client = new HttpClient();
        var disco = await client.GetDiscoveryDocumentAsync("https://localhost:5002");

        if (disco.IsError)
        {
            return BadRequest("Error while discovering IdentityServer.");
        }

        var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
        {
            Address = disco.TokenEndpoint,
            ClientId = "airmastera-web-api",
            Scope = "openid profile AirMasteraWebAPI"
        });

        if (tokenResponse.IsError)
        {
            return BadRequest("Error while requesting token.");
        }

        return Ok(tokenResponse.AccessToken);
    }

    /// <summary>
    /// Создание авто
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    [HttpPost]
    [Route("Create")]
    public async Task<ActionResult> Create([FromQuery] [Required] Guid personId, CreateOrUpdateCarRequest request, CancellationToken cancellationToken)
    {
        return Ok(await _carService.CreateCar(personId, request, cancellationToken));
    }

    /// <summary>
    /// Обновление авто
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    [HttpPost]
    [Route("Update")]
    public async Task<ActionResult> Update([FromQuery] [Required] Guid carId, [FromBody] CreateOrUpdateCarRequest request,
        CancellationToken cancellationToken)
    {
        return Ok(await _carService.UpdateCarAsync(carId, request, cancellationToken));
    }

    /// <summary>
    /// Получение авто по personId
    /// </summary>
    /// /// <param name="personId"></param>
    /// <param name="pageNumber"></param>
    /// <param name="pageSize"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("GetByPersonId")]
    public async Task<ActionResult> Get([FromQuery] [Required] Guid personId, CancellationToken cancellationToken, int pageNumber = 1, int pageSize = 20)
    {
        return Ok(await _carService.GetCarsAsync(personId, pageNumber, pageSize, cancellationToken));
    }

    /// <summary>
    /// Получение авто по id
    /// </summary>
    /// <param name="carId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("GetById")]
    public async Task<ActionResult> GetById([FromQuery] Guid carId, CancellationToken cancellationToken)
    {
        return Ok(await _carService.GetCarAsync(carId, cancellationToken));
    }

    /// <summary>
    /// Удалить авто
    /// </summary>
    /// <param name="carId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpDelete]
    [Route("Delete")]
    public async Task Delete([FromQuery] [Required] Guid carId, CancellationToken cancellationToken)
    {
        await _carService.DeleteCar(carId, cancellationToken);
    }
}