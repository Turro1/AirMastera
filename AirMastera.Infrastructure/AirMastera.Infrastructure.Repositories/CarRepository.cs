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

public class CarRepository : ICarRepository
{
    private readonly AirMasteraDbContext _dbContext;
    private readonly IMapper _mapper;

    public CarRepository(AirMasteraDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<IEnumerable<CarDto>> GetCarsAsync(Guid personId, int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        var cars = await _dbContext.Cars.AsNoTracking()
            .Where(x => x.PersonDbId == personId)
            .ProjectTo<CarDto>(_mapper.ConfigurationProvider)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToArrayAsync(cancellationToken);

        return cars;
    }

    public async Task<CarDto> GetCarAsync(Guid carId, CancellationToken cancellationToken)
    {
        var carDb = await GetCarDb(carId, cancellationToken);

        return _mapper.Map<CarDto>(carDb);
    }

    public async Task<CarDto> UpdateCarAsync(Car car, CancellationToken cancellationToken)
    {
        var updatedPersonDb = _mapper.Map<CarDb>(car);

        var personId = (await GetCarDb(car.Id, cancellationToken)).PersonDbId;
        updatedPersonDb.PersonDbId = personId;

        _dbContext.Update(updatedPersonDb);

        if (updatedPersonDb.Repairs?.Any() == true)
        {
            var oldCars = await _dbContext.Repairs.AsNoTracking()
                .Where(carDb => carDb.CarDbId == updatedPersonDb.Id)
                .ToListAsync(cancellationToken);

            foreach (var carDb in updatedPersonDb.Repairs)
            {
                if (oldCars.Any(we => we.Id == carDb.Id))
                {
                    _dbContext.Update(carDb);
                }
                else
                {
                    _dbContext.Add(carDb);
                }
            }
        }

        await _dbContext.SaveChangesAsync(cancellationToken);

        return _mapper.Map<CarDto>(updatedPersonDb);
    }

    public async Task DeleteCar(Guid carId, CancellationToken cancellationToken)
    {
        var car = await GetCarDb(carId, cancellationToken);
        _dbContext.Cars.Remove(car);
    }

    private async Task<CarDb> GetCarDb(Guid id, CancellationToken cancellationToken)
    {
        var carDb = await _dbContext.Cars.AsNoTracking().Include(x => x.Repairs)
            .FirstOrDefaultAsync(car =>
                car.Id == id, cancellationToken);

        if (carDb == null)
        {
            throw new NotFoundException("Сущность c id: " + id + " не найдена в базе данных...");
        }

        return carDb;
    }
}