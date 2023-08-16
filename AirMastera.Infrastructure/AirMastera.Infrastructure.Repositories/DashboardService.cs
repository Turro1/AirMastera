using AirMastera.Application.Services.Interfaces;
using AirMastera.Application.Services.Models;
using AirMastera.Domain.Entities.Extensions;
using AirMastera.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AirMastera.Infrastructure.Repositories;

public class DashboardService : IDashboardService
{
    private readonly AirMasteraDbContext _dbContext;

    public DashboardService(AirMasteraDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task<IEnumerable<DashboardInfo>> GetDashboardInfo(CancellationToken cancellationToken)
    {
        var persons = _dbContext.Persons
            .Include(x => x.Cars)
            .ThenInclude(carDb => carDb.Repairs);

        return await (from person in persons
            where person.Cars != null && person.Cars.Any()
            from car in person.Cars
            where car.Repairs != null && car.Repairs.Any()
            from repair in car.Repairs
            select new DashboardInfo
            {
                PhoneNumber = person.Phone,
                Avatar = car.Avatar,
                CarName = car.Name,
                CarModel = car.Model,
                AppointmentDate = repair.AppointmentDate,
                RepairStatus = repair.RepairStatus.ToText(),
                Id = repair.Id
            }).ToArrayAsync(cancellationToken);
    }
}