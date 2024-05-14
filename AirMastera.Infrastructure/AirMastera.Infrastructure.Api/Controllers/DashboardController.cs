using AirMastera.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AirMastera.Infrastructure.Api.Controllers;

[ApiController]
//[Authorize]
[Route("api/[controller]")]
public class DashboardController : ControllerBase
{
    private readonly IDashboardService _dashboardService;

    public DashboardController(IDashboardService dashboardService)
    {
        _dashboardService = dashboardService ?? throw new ArgumentNullException(nameof(dashboardService));
    }

    [HttpGet]
    [Route("GetDashboardInfo")]
    public async Task<ActionResult> GetDashboardInfo(CancellationToken cancellationToken)
    {
        return Ok(await _dashboardService.GetDashboardInfo(cancellationToken));
    }
}