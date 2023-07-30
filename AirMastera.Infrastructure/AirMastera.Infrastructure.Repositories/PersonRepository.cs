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

public class PersonRepository : IPersonRepository
{
    private readonly AirMasteraDbContext _dbContext;
    private readonly IMapper _mapper;

    public PersonRepository(IMapper mapper, AirMasteraDbContext dbContext)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task<PersonDto> CreatePersonAsync(Person request, CancellationToken cancellationToken)
    {
        var personDb = _mapper.Map<PersonDb>(request);
        _dbContext.Persons.Add(personDb);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return _mapper.Map<PersonDto>(personDb);
    }

    public async Task<PersonDto> UpdatePersonAsync(Person person, CancellationToken cancellationToken)
    {
        var updatedPersonDb = _mapper.Map<PersonDb>(person);

        _dbContext.Update(updatedPersonDb);

        if (updatedPersonDb.Cars?.Any() == true)
        {
            var oldCars = await _dbContext.Cars.AsNoTracking()
                .Where(carDb => carDb.PersonDbId == updatedPersonDb.Id)
                .ToListAsync(cancellationToken);

            foreach (var carDb in updatedPersonDb.Cars)
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

        return _mapper.Map<PersonDto>(updatedPersonDb);
    }

    public async Task UpdateCarAsync(Car car, CancellationToken cancellationToken)
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
    }

    public async Task<PersonDto> GetPersonDtoAsync(Guid id, CancellationToken cancellationToken)
    {
        var personDb = await GetPersonDb(id, cancellationToken);

        return _mapper.Map<PersonDto>(personDb);
    }

    public async Task<Person> GetPersonAsync(Guid id, CancellationToken cancellationToken)
    {
        var personDb = await GetPersonDb(id, cancellationToken);

        return _mapper.Map<Person>(personDb);
    }

    public Task<IEnumerable<PersonDb>> GetAllPersonsAsync(CancellationToken cancellationToken)
    {
        var personsDb = _dbContext.Persons.AsQueryable();
        var persons = personsDb.>().
        
        return persons;
    }

    private async Task<PersonDb> GetPersonDb(Guid id, CancellationToken cancellationToken)
    {
        var personDb = await _dbContext.Persons.AsNoTracking().Include(db => db.Cars)
            .FirstOrDefaultAsync(person =>
                person.Id == id, cancellationToken);

        if (personDb == null)
        {
            throw new NotFoundException("Сущность c id: " + id + " не найдена в базе данных...");
        }

        return personDb;
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

    public async Task<CarDto> GetCarDtoAsync(Guid id, CancellationToken cancellationToken)
    {
        var carDb = await GetCarDb(id, cancellationToken);

        return _mapper.Map<CarDto>(carDb);
    }

    public async Task<Car> GetCarAsync(Guid id, CancellationToken cancellationToken)
    {
        var carDb = await GetCarDb(id, cancellationToken);

        return _mapper.Map<Car>(carDb);
    }

    public async Task DeletePersonAsync(Guid id, CancellationToken cancellationToken)
    {
        var personDb = await GetPersonDb(id, cancellationToken);

        _dbContext.Remove(personDb);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}