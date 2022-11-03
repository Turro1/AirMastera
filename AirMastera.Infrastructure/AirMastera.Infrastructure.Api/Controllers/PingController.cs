using Microsoft.AspNetCore.Mvc;

namespace AirMastera.Infrastructure.Api.Controllers;

/// <summary>
/// Контроллер для получения pong
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class PingController : ControllerBase
{
    /// <summary>
    /// Получение "pong"
    /// </summary>
    /// <returns>pong</returns>
    [HttpGet]
    public string Ping()
    {
        return "pong";
    }
}