using AirMastera.Application.Services.Interfaces;
using AirMastera.Application.Services.Models;
using AirMastera.Domain.Entities;
using AirMastera.Domain.Exceptions;
using AirMastera.Infrastructure.Data;
using AirMastera.Infrastructure.Data.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace AirMastera.Infrastructure.Repositories;

public class RepairRepository : IRepairRepository
{
    private readonly AirMasteraDbContext _dbContext;
    private readonly IMapper _mapper;

    public RepairRepository(AirMasteraDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<IEnumerable<RepairDto>> GetRepairs(Guid carId, int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        var repairs = await _dbContext.Repairs.AsNoTracking()
            .Where(x => x.CarDbId == carId)
            .ProjectTo<RepairDto>(_mapper.ConfigurationProvider)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToArrayAsync(cancellationToken);

        return repairs;
    }

    public async Task<RepairDto> GetRepair(Guid repairId, CancellationToken cancellationToken)
    {
        var currentRepair = await _dbContext.Repairs.FirstOrDefaultAsync(x => x.Id == repairId, cancellationToken);
        if (currentRepair == null)
        {
            throw new NotFoundException($"Сущность {currentRepair?.GetType()} c id: {repairId} не найдена");
        }

        return _mapper.Map<RepairDto>(currentRepair);
    }

    public async Task<RepairDto> UpdateRepairAsync(Repair repair, CancellationToken cancellationToken)
    {
        var updatedRepairDb = _mapper.Map<RepairDb>(repair);
        var personId = (await GetRepairDb(repair.Id, cancellationToken)).CarDbId;
        updatedRepairDb.CarDbId = personId;

        _dbContext.Update(updatedRepairDb);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return _mapper.Map<RepairDto>(updatedRepairDb);
    }

    public async Task DeleteRepair(Guid repairId, CancellationToken cancellationToken)
    {
        var repairDb = await GetRepairDb(repairId, cancellationToken);

        _dbContext.Repairs.Remove(repairDb);
    }

    private async Task<RepairDb> GetRepairDb(Guid id, CancellationToken cancellationToken)
    {
        var repairDb = await _dbContext.Repairs.AsNoTracking()
            .FirstOrDefaultAsync(repair =>
                repair.Id == id, cancellationToken);

        if (repairDb == null)
        {
            throw new NotFoundException("Сущность c id: " + id + " не найдена в базе данных...");
        }

        return repairDb;
    }
}