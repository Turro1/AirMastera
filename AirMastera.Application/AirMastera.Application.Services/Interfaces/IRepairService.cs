using AirMastera.Application.Services.Models;

namespace AirMastera.Application.Services.Interfaces;

public interface IRepairService
{
    Task<RepairDto> CreateRepairAsync(Guid carId, CreateOrUpdateRepairRequest request, CancellationToken cancellationToken);
    Task<RepairDto> UpdateRepairAsync(Guid repairId, CreateOrUpdateRepairRequest request, CancellationToken cancellationToken);
    Task<IEnumerable<RepairDto>> GetRepairs(Guid carId, int pageNumber, int pageSize, CancellationToken cancellationToken);
    Task<RepairDto> GetRepair(Guid repairId, CancellationToken cancellationToken);
    Task DeleteRepairAsync(Guid repairId, CancellationToken cancellationToken);
}