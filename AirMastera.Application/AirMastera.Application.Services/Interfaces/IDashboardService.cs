using AirMastera.Application.Services.Models;

namespace AirMastera.Application.Services.Interfaces;

public interface IDashboardService
{
    Task<IEnumerable<DashboardInfo>> GetDashboardInfo(CancellationToken cancellationToken);
}