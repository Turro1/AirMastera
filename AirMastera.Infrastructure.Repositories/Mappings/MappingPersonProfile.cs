using AutoMapper;

namespace AirMastera.Infrastructure.Repositories.Mappings;

public class MappingPersonProfile : Profile
{
    public MappingPersonProfile()
    {
        /*CreateMap<PersonDb, Person>()
            .ConstructUsing(personDb =>
                new Person(
                    personDb.Id,
                    new FullName(personDb.Surname, personDb.Name, personDb.SecondName),
                    new Email(personDb.Email),
                    new Phone(personDb.Phone),
                    personDb.BirthDay,
                    new Uri(personDb.Avatar),
                    personDb.Gender,
                    personDb.Comment)
            )
            .AfterMap((personDb, person) =>
            {
                foreach (var item in personDb.WorkExperiences!)
                {
                    person.SaveWorkExperience(item.Id, item.Position, item.Organization, new Address(item.City, item.Country), item.Description!,
                        item.DateEmployment, item.DateDismissal);
                }
            })
            .ForAllMembers(p => p.Ignore());

        CreateMap<Person, PersonDb>()
            .ForMember(personDb => personDb.Phone,
                o => o
                    .MapFrom(person => person.Phone.PhoneNumber))
            .ForMember(personDb => personDb.Email,
                o => o
                    .MapFrom(person => person.Email.EmailAddress))
            .ConstructUsing((person, context) => new PersonDb
            {
                Id = person.Id,
                Surname = person.FullName.Surname,
                Name = person.FullName.Name,
                SecondName = person.FullName.SecondName,
                Phone = person.Phone.PhoneNumber,
                Email = person.Email.EmailAddress,
                Avatar = person.Avatar.AbsoluteUri,
                BirthDay = person.BirthDay,
                Comment = person.Comment,
                Gender = person.Gender,
                WorkExperiences = context.Mapper.Map<IEnumerable<WorkExperienceDb>>(person.WorkExperiences)
            })
            .AfterMap((person, personDb) =>
            {
                foreach (var item in personDb.WorkExperiences!)
                {
                    item.PersonDbId = person.Id;
                }
            });

        CreateMap<CreatePersonRequest, Person>()
            .ConstructUsing(person =>
                new Person(
                    person.Id,
                    new FullName(person.Surname, person.Name, person.SecondName),
                    new Email(person.Email),
                    new Phone(person.Phone),
                    person.BirthDay,
                    new Uri(person.Avatar),
                    person.Gender,
                    person.Comment));

        CreateMap<PersonDb, PersonDto>()
            .ConstructUsing((person, context) =>
                new PersonDto
                {
                    Id = person.Id,
                    Surname = person.Surname,
                    Name = person.Name,
                    SecondName = person.SecondName,
                    Phone = person.Phone,
                    Email = person.Email,
                    Avatar = person.Avatar,
                    BirthDay = person.BirthDay,
                    Comment = person.Comment,
                    Gender = person.Gender,
                    WorkExperiences = context.Mapper.Map<IEnumerable<WorkExperienceDto>>(person.WorkExperiences)
                });

        CreateMap<WorkExperienceDb, WorkExperienceDto>()
            .ConstructUsing((workExperience) =>
                new WorkExperienceDto
                {
                    Id = workExperience.Id,
                    Position = workExperience.Position,
                    Organization = workExperience.Organization,
                    City = workExperience.City,
                    Country = workExperience.Country,
                    Description = workExperience.Description,
                    DateEmployment = workExperience.DateEmployment,
                    DateDismissal = workExperience.DateDismissal,
                    PersonId = workExperience.PersonDbId
                });

        CreateMap<PersonDto, UpdatePersonRequest>()
            .ConstructUsing((person) =>
                new UpdatePersonRequest
                {
                    Id = person.Id,
                    Surname = person.Surname,
                    Name = person.Name,
                    SecondName = person.SecondName,
                    Phone = person.Phone,
                    Email = person.Email,
                    Avatar = person.Avatar,
                    BirthDay = person.BirthDay,
                    Comment = person.Comment,
                    Gender = person.Gender,
                });

        CreateMap<SaveWorkExperienceRequest, WorkExperienceDb>()
            .ConstructUsing((workExperience) =>
                new WorkExperienceDb
                {
                    Id = Guid.Empty,
                    Position = workExperience.Position,
                    Organization = workExperience.Organization,
                    City = workExperience.City,
                    Country = workExperience.Country,
                    Description = workExperience.Description,
                    DateEmployment = workExperience.DateEmployment,
                    DateDismissal = workExperience.DateDismissal,
                    PersonDbId = workExperience.PersonId
                });

        CreateMap<WorkExperience, WorkExperienceDb>()
            .ConstructUsing((workExperience) =>
                new WorkExperienceDb
                {
                    Id = workExperience.Id,
                    Position = workExperience.Position,
                    Organization = workExperience.Organization,
                    City = workExperience.Address.City,
                    Country = workExperience.Address.Country,
                    Description = workExperience.Description,
                    DateEmployment = workExperience.DateEmployment,
                    DateDismissal = workExperience.DateDismissal,
                });*/
    }
}