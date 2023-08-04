using AirMastera.Application.Services.Models;
using AirMastera.Domain.Entities;

namespace AirMastera.Application.Services.Interfaces;

public interface IRepairRepository
{
    Task<IEnumerable<RepairDto>> GetRepairs(Guid carId, int pageNumber, int pageSize, CancellationToken cancellationToken);
    Task<RepairDto> GetRepair(Guid repairId, CancellationToken cancellationToken);
    Task<RepairDto> UpdateRepairAsync(Repair repair, CancellationToken cancellationToken);
    Task DeleteRepair(Guid repairId, CancellationToken cancellationToken);
}