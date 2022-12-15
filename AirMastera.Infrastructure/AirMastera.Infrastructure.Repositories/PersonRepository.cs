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
        _mapper = mapper;
        _dbContext = dbContext;
    }

    public async Task CreatePersonAsync(Person request, CancellationToken cancellationToken)
    {
        var personDb = _mapper.Map<PersonDb>(request);
        _dbContext.Persons.Add(personDb);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public Task UpdatePersonAsync(Person person, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
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

    public Task<PersonDto> DeletePersonDtoAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
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
}