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

        CreateMap<UpdateCarRequest, Car>()
            .AfterMap((updateRequest, car) =>
            {
                var repair = updateRequest.Repair;
                if (repair != null)
                    car.SaveRepair(repair.Id, repair.PartName, repair.PartType, repair.Price, repair.AppointmentDate);
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
                if (personDb.Cars?.Any() == true)
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

        CreateMap<CarDb, Car>()
            .ConstructUsing(carDb =>
                new Car(
                    carDb.Id,
                    carDb.Name,
                    carDb.Model,
                    carDb.Number,
                    carDb.Avatar)
            )
            .AfterMap((personDb, person) =>
            {
                foreach (var item in personDb.Repairs!)
                {
                    person.SaveRepair(item.Id, item.PartName, item.PartType, item.Price, item.AppointmentDate);
                }
            })
            .ForAllMembers(p => p.Ignore());

        CreateMap<Car, CarDb>()
            .ConstructUsing((car, context) =>
                new CarDb
                {
                    Id = car.Id,
                    Name = car.Name,
                    Model = car.Model,
                    Number = car.Number,
                    Avatar = car.Avatar,
                    Repairs = context.Mapper.Map<IEnumerable<RepairDb>>(car.Repairs)
                })
            .AfterMap((car, carDb) =>
            {
                foreach (var item in carDb.Repairs!)
                {
                    item.CarDbId = car.Id;
                }
            });

        CreateMap<Repair, RepairDb>()
            .ConstructUsing(repair =>
                new RepairDb
                {
                    Id = repair.Id,
                    PartName = repair.PartName,
                    PartType = repair.PartType,
                    Price = repair.Price,
                    AppointmentDate = repair.AppointmentDate,
                });
    }
}