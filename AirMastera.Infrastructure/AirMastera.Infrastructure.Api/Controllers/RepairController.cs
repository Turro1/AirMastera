using System.ComponentModel.DataAnnotations;
using AirMastera.Application.Services.Interfaces;
using AirMastera.Application.Services.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AirMastera.Infrastructure.Api.Controllers;

/// <summary>
/// Контроллер для получения pong
/// </summary>
[ApiController]
[Authorize]
[Route("api/[controller]")]
public class RepairController : ControllerBase
{
    private readonly IRepairService _repairService;

    public RepairController(IRepairService repairService)
    {
        _repairService = repairService ?? throw new ArgumentNullException(nameof(repairService));
    }

    /// <summary>
    /// Создание ремонта
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    [HttpPost]
    [Route("Create")]
    public async Task<ActionResult> Create(Guid carId, CreateOrUpdateRepairRequest request, CancellationToken cancellationToken)
    {
        return Ok(await _repairService.CreateRepairAsync(carId, request, cancellationToken));
    }

    /// <summary>
    /// Обновление ремонта
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    [HttpPost]
    [Route("Update")]
    public async Task<ActionResult> Update([FromQuery] [Required] Guid repairId, [FromBody] CreateOrUpdateRepairRequest request,
        CancellationToken cancellationToken)
    {
        return Ok(await _repairService.UpdateRepairAsync(repairId, request, cancellationToken));
    }

    /// <summary>
    /// Получение ремонтов
    /// </summary>
    /// <param name="pageNumber"></param>
    /// <param name="pageSize"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("GetByCarId")]
    public async Task<ActionResult> GetByCarId(Guid carId, CancellationToken cancellationToken, int pageNumber = 1, int pageSize = 20)
    {
        return Ok(await _repairService.GetRepairs(carId, pageNumber, pageSize, cancellationToken));
    }

    /// <summary>
    /// Получение ремонта по id
    /// </summary>
    /// <param name="repairId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("GetById")]
    public async Task<ActionResult> GetById([FromQuery] Guid repairId, CancellationToken cancellationToken)
    {
        return Ok(await _repairService.GetRepair(repairId, cancellationToken));
    }

    /// <summary>
    /// Удалить ремонт
    /// </summary>
    /// <param name="repairId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpDelete]
    [Route("Delete")]
    public async Task Delete([FromQuery] [Required] Guid repairId, CancellationToken cancellationToken)
    {
        await _repairService.DeleteRepairAsync(repairId, cancellationToken);
    }
}