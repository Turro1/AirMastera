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
        _personRepository = personRepository ?? throw new ArgumentNullException(nameof(personRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task CreatePersonAsync(CreatePersonRequest personRequest, CancellationToken cancellationToken)
    {
        var person = _mapper.Map<Person>(personRequest);

        await _personRepository.CreatePersonAsync(person, cancellationToken);
    }

    public async Task<CarDto> SaveWorkExperienceAsync(Guid id, SaveCarRequest car, CancellationToken cancellationToken)
    {
        var person = await _personRepository.GetPersonAsync(car.PersonId, cancellationToken);

        person.SaveCar(id, car.Name, car.Model, car.Number, car.Avatar);

        await _personRepository.UpdatePersonAsync(person, cancellationToken);
        return await _personRepository.GetCarDtoAsync(id, cancellationToken);
    }

    public async Task UpdatePersonAsync(UpdatePersonRequest request, CancellationToken cancellationToken)
    {
        var person = _mapper.Map<Person>(request);

        await _personRepository.UpdatePersonAsync(person, cancellationToken);
    }

    public async Task<PersonDto> GetPersonDtoAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _personRepository.GetPersonDtoAsync(id, cancellationToken);
    }

    public async Task<Person> GetPersonAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _personRepository.GetPersonAsync(id, cancellationToken);
    }

    public async Task<CarDto> SaveCarAsync(Guid id, SaveCarRequest car, CancellationToken cancellationToken)
    {
        {
            var person = await _personRepository.GetPersonAsync(car.PersonId, cancellationToken);

            person.SaveCar(id, car.Name, car.Model, car.Number, car.Avatar);

            await _personRepository.UpdatePersonAsync(person, cancellationToken);
            return await _personRepository.GetCarDtoAsync(id, cancellationToken);
        }
    }

    public async Task DeletePersonAsync(Guid id, CancellationToken cancellationToken)
    {
        await _personRepository.DeletePersonAsync(id, cancellationToken);
    }
}