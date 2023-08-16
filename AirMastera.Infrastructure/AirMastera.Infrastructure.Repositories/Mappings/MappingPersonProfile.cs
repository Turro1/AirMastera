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
                    person.SaveCar(car.Id ?? Guid.NewGuid(), car.Name, car.Model, car.Number, car.Avatar);
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
                new Person(
                    Guid.NewGuid(),
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

        CreateMap<PersonDb, PersonDto>()
            .ConstructUsing((x, context) =>
                new PersonDto
                {
                    Id = x.Id,
                    FullName = x.FullName,
                    Phone = x.Phone,
                    Cars = context.Mapper.Map<IEnumerable<CarDto>>(x.Cars)
                });

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

        CreateMap<CreateOrUpdateCarRequest, Car>()
            .AfterMap((request, car) =>
            {
                if (request.Repair != null)
                {
                    car.SaveRepair(request.Repair.Id, request.Repair.PartName, request.Repair.PartType, request.Repair.Price, request.Repair.AppointmentDate);
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

        CreateMap<Car, CarDto>();

        CreateMap<CarDb, CarDto>()
            .ConstructUsing((x, context) =>
                new CarDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Model = x.Model,
                    Number = x.Number,
                    Avatar = x.Avatar,
                    Repairs = context.Mapper.Map<IEnumerable<RepairDto>>(x.Repairs)
                });

        CreateMap<Repair, RepairDb>()
            .ConstructUsing(repair =>
                new RepairDb
                {
                    Id = repair.Id,
                    PartName = repair.PartName,
                    PartType = repair.PartType,
                    Price = repair.Price,
                    RepairStatus = repair.RepairStatus,
                    AppointmentDate = repair.AppointmentDate
                });

        CreateMap<RepairDb, RepairDto>()
            .ForMember(x => x.Id,
                expression => expression.MapFrom(x => x.Id))
            .ForMember(x => x.AppointmentDate,
                expression => expression.MapFrom(x => x.AppointmentDate))
            .ForMember(x => x.PartName,
                expression => expression.MapFrom(x => x.PartName))
            .ForMember(x => x.PartType,
                expression => expression.MapFrom(x => x.PartType))
            .ForMember(x => x.Price,
                expression => expression.MapFrom(x => x.Price));

        CreateMap<CreateOrUpdateRepairRequest, Repair>()
            .ForMember(x => x.PartName,
                expression => expression.MapFrom(x => x.PartName))
            .ForMember(x => x.PartType,
                expression => expression.MapFrom(x => x.PartType))
            .ForMember(x => x.Price,
                expression => expression.MapFrom(x => x.Price))
            .ForMember(x => x.AppointmentDate,
                expression => expression.MapFrom(x => x.AppointmentDate));
    }
}