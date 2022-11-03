using AirMastera.Application.Services.Interfaces;
using AirMastera.Application.Services.Models;
using AirMastera.Domain.Entities;
using AutoMapper;

namespace AirMastera.Application.Services.Services;

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

    public Task<PersonDto> DeletePersonDtoAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}