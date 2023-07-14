using AirMastera.Application.Services.Models;
using AirMastera.Domain.Entities;
using AirMastera.Infrastructure.Data.Models;
using AutoMapper;

namespace AirMastera.Infrastructure.Repositories.Mappings;

public class MappingPersonProfile : Profile
{
    public MappingPersonProfile()
    {
        CreateMap<UpdatePersonRequest, Person>()
            .AfterMap((updateRequest, person) =>
            {
                var car = updateRequest.Car;
                if (car != null)
                    person.SaveCar(car.Id, car.Name, car.Model, car.Number, car.Avatar);
            });

        CreateMap<CreatePersonRequest, Person>()
            .ConstructUsing(person =>
                new Person(person.Id,
                    person.FullName,
                    person.Phone));

        CreateMap<PersonDb, Person>()
            .ConstructUsing(personDb =>
                new Person(
                    personDb.Id,
                    personDb.FullName,
                    personDb.Phone)
            )
            .AfterMap((personDb, person) =>
            {
                foreach (var item in personDb.Cars!)
                {
                    person.SaveCar(item.Id, item.Name, item.Model, item.Number, item.Avatar);
                }
            })
            .ForAllMembers(p => p.Ignore());

        CreateMap<Person, PersonDb>()
            .ConstructUsing((person, context) => new PersonDb
            {
                Id = person.Id,
                FullName = person.FullName,
                Phone = person.Phone,
                Cars = context.Mapper.Map<IEnumerable<CarDb>>(person.Cars)
            })
            .AfterMap((person, personDb) =>
            {
                foreach (var item in personDb.Cars!)
                {
                    item.PersonDbId = person.Id;
                }
            });

        CreateMap<Car, CarDb>()
            .ConstructUsing((workExperience) =>
                new CarDb
                {
                    Id = workExperience.Id,
                    Name = workExperience.Name,
                    Model = workExperience.Model,
                    Number = workExperience.Number,
                    Avatar = workExperience.Avatar,
                });
    }
}