using Bogus;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using AirMastera.Application.Services.Models;
using AirMastera.Infrastructure.Data.Models;

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
        var person = new Faker<PersonDb>("ru")
            .RuleFor(u => u.FullName, x => (x.Name.FirstName() + " " + x.Name.LastName()))
            .RuleFor(u => u.Phone, f => f.Phone.PhoneNumber("+373777#####"));

        var createUser = person.Generate();

        var createPerson = new CreatePersonRequest
        {
            Id = Guid.NewGuid(),
            Phone = createUser.Phone,
            FullName = createUser.FullName
        };

        yield return new object[]
        {
            createPerson
        };
    }

    public static IEnumerable<object[]> CreateAndUpdatePersonParameters()
    {
        var person = new Faker<PersonDb>("ru")
            .RuleFor(u => u.FullName, x => (x.Name.FirstName() + " " + x.Name.LastName()))
            .RuleFor(u => u.Phone, f => f.Phone.PhoneNumber("+373777#####"));

        var updateUser = person.Generate();
        var createUser = person.Generate();

        var createPerson = new CreatePersonRequest
        {
            Id = Guid.NewGuid(),
            Phone = createUser.Phone,
            FullName = createUser.FullName
        };

        var updatePerson = new UpdatePersonRequest
        {
            Phone = updateUser.Phone,
            FullName = updateUser.FullName
        };

        yield return new object[]
        {
            createPerson,
            updatePerson
        };
    }

    public static IEnumerable<object[]> RemovePersonParameters()
    {
        var person = new Faker<PersonDb>("ru")
            .RuleFor(u => u.FullName, x => (x.Name.FirstName() + " " + x.Name.LastName()))
            .RuleFor(u => u.Phone, f => f.Phone.PhoneNumber("+373777#####"));

        var createUser = person.Generate();

        var createPerson = new UpdatePersonRequest
        {
            Phone = createUser.Phone,
            FullName = createUser.FullName
        };

        yield return new object[]
        {
            createPerson.Id
        };
    }

    public static IEnumerable<object[]> CreateCarParameters()
    {
        var car = new Faker<CarDb>("ru")
            .RuleFor(u => u.Name, x => x.Vehicle.Manufacturer())
            .RuleFor(u => u.Model, f => f.Vehicle.Model());

        var createCar = car.Generate();

        var createPerson = new SaveCarRequest
        {
            Id = Guid.NewGuid(),
            /*Phone = createCar.Phone,
            FullName = createCar.FullName*/
        };

        yield return new object[]
        {
            createPerson,
        };
    }
}