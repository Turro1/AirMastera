using AirMastera.Application.Services.Models;
using AirMastera.Domain.Entities;
using AirMastera.Infrastructure.Data.Models;
using AutoMapper;

namespace AirMastera.Infrastructure.Repositories.Mappings;

public class MappingPersonProfile : Profile
{
    public MappingPersonProfile()
    {
        CreateMap<CreatePersonRequest, Person>()
            .ConstructUsing(person =>
                new Person(person.Id,
                    person.FullName,
                    person.FullName));

        CreateMap<PersonDb, Person>()
            .ConstructUsing(personDb =>
                new Person(personDb.Id, personDb.FullName, personDb.Phone));
        CreateMap<Person, PersonDb>()
            .ConstructUsing(person =>
                new PersonDb()
                {
                    FullName = person.FullName,
                    Phone = person.Phone
                });

        CreateMap<PersonDb, Person>()
            .ForMember(p => p.Id, o =>
                o.MapFrom(p => p.Id))
            .ForMember(p => p.Phone, o =>
                o.MapFrom(p => p.Phone))
            .ForMember(p => p.FullName, o =>
                o.MapFrom(p => p.FullName));
    }
}