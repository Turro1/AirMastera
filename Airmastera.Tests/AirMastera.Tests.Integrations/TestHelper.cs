using Bogus;
using Bogus.DataSets;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using AirMastera.Application.Services.Models;
using AirMastera.Infrastructure.Data.Models;
using AirMastera.Domain.Entities.HelpingClasses;

namespace Integrations;

/// <summary>
/// Класс создания параметров для Person
/// </summary>
public static class TestHelper
{
    /// <summary>
    /// Когда базы данных не существовало, пользовательских типов (например, enum) тоже не существовало,
    /// поэтому нужно перезагрузить типы после миграции базы данных
    /// </summary>
    /// <param name="dbContext">DbContext</param>
    public static void MigrateAndReloadTypes(this DbContext dbContext)
    {
        dbContext.Database.Migrate();
        var dbConnection = (NpgsqlConnection) dbContext.Database.GetDbConnection();
        dbConnection.Open();
        dbConnection.ReloadTypes();
        dbConnection.Close();
    }

    /// <summary>
    /// Создание параметров для Person
    /// </summary>
    /// <returns></returns>
    public static IEnumerable<object[]> CreatePersonParameters()
    {
        string[] secondNames = new[] {"Александрович", "Петрович", "Николаевич", "Иванович", "Олегович"};
        string[] avatars = new[]
        {
            "http://dog.jpg", "http://cat.jpg", "http://motorcycle.png",
            "http://car.jpg", "http://military.png"
        };

        var person = new Faker<PersonDb>("ru")
            .RuleFor(u => u.Gender, f => (Gender) f.Person.Gender)
            .RuleFor(u => u.Surname, (f, u) => f.Name.LastName((Name.Gender) u.Gender))
            .RuleFor(u => u.Name, (f, u) => f.Name.FirstName((Name.Gender) u.Gender))
            .RuleFor(u => u.SecondName, f => f.PickRandom(secondNames))
            .RuleFor(u => u.Avatar, f => f.PickRandom(avatars))
            .RuleFor(u => u.BirthDay, f => f.Date.Between(new DateTime(1900, 5, 30), new DateTime(2003, 5, 30)))
            .RuleFor(u => u.Phone, f => f.Phone.PhoneNumber("+373777#####"))
            .RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.Name, u.Surname));

        var createUser = person.Generate();

        var createPerson = new CreatePersonRequest
        {
            Id = Guid.NewGuid(),
            Surname = createUser.Surname,
            Name = createUser.Name,
            SecondName = createUser.SecondName,
            Email = createUser.Email,
            BirthDay = createUser.BirthDay,
            Phone = createUser.Phone,
            Avatar = createUser.Avatar,
            Gender = createUser.Gender,
            Comment = createUser.Comment
        };

        yield return new object[]
        {
            createPerson,
        };
    }
}