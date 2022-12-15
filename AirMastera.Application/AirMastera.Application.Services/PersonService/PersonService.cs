using AirMastera.Application.Services.Interfaces;
using AirMastera.Application.Services.Models;
using AirMastera.Domain.Entities;
using AutoMapper;

namespace AirMastera.Application.Services.PersonService;

public class PersonService : IPersonService
{
    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;

    public PersonService(IPersonRepository personRepository, IMapper mapper)
    {
        _personRepository = personRepository;
        _mapper = mapper;
    }

    public async Task CreatePersonAsync(CreatePersonRequest personRequest, CancellationToken cancellationToken)
    {
        var person = _mapper.Map<Person>(personRequest);

        await _personRepository.CreatePersonAsync(person, cancellationToken);
    }

    public Task UpdatePersonAsync(Person person, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<PersonDto> GetPersonDtoAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _personRepository.GetPersonDtoAsync(id, cancellationToken);
    }

    public async Task<Person> GetPersonAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _personRepository.GetPersonAsync(id, cancellationToken);
    }

    public Task<PersonDto> DeletePersonDtoAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<CarDto> SaveCarAsync(Guid id, SaveCarRequest car, CancellationToken cancellationToken)
    {
        {
            var person = await _personRepository.GetPersonAsync(car.PersonId, cancellationToken);

            person.SaveCar(id, car.Name, car.Model, car.Number,car.Avatar);

            await _personRepository.UpdatePersonAsync(person, cancellationToken);
            return await _personRepository.GetCarDtoAsync(id, cancellationToken);
        }
    }
}