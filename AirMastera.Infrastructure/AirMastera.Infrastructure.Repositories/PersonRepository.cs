using AirMastera.Application.Services.Interfaces;
using AirMastera.Application.Services.Models;
using AirMastera.Domain.Entities;
using AirMastera.Domain.Exceptions;
using AirMastera.Infrastructure.Data;
using AirMastera.Infrastructure.Data.Models;
using AutoMapper;
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

    public async Task CreatePersonAsync(Person request, CancellationToken cancellationToken)
    {
        var personDb = _mapper.Map<PersonDb>(request);
        _dbContext.Persons.Add(personDb);

        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdatePersonAsync(Person person, CancellationToken cancellationToken)
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
    }

    public async Task<PersonDto> GetPersonDtoAsync(Guid id, CancellationToken cancellationToken)
    {
        var personDb = await GetPersonDb(id, cancellationToken);

        var personDto = _mapper.Map<PersonDto>(personDb);
        return personDto;
    }

    public async Task<Person> GetPersonAsync(Guid id, CancellationToken cancellationToken)
    {
        var personDb = await GetPersonDb(id, cancellationToken);

        var person = _mapper.Map<Person>(personDb);
        return person;
    }

    private async Task<PersonDb> GetPersonDb(Guid id, CancellationToken cancellationToken)
    {
        var personDb = await _dbContext.Persons //.AsNoTracking().Include(db => db.Cars)
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
        var carDb = await _dbContext.Cars.AsNoTracking()
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

        var personDto = _mapper.Map<CarDto>(carDb);
        return personDto;
    }

    public async Task DeletePersonAsync(Guid id, CancellationToken cancellationToken)
    {
        var personDb = await GetPersonDb(id, cancellationToken);

        _dbContext.Remove(personDb);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}