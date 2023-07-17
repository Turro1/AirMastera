using AirMastera.Application.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using AirMastera.Application.Services.Models;
using AirMastera.Domain.Exceptions;
using AirMastera.Infrastructure.Data;
using FluentAssertions;
using Xunit;

namespace Integrations;

public class IntegrationsTests : IDisposable
{
    private readonly IServiceProvider _serviceProvider;
    private readonly CancellationToken _cancellationToken;
    private readonly IPersonService _service;

    public IntegrationsTests()
    {
        _serviceProvider = new MyServiceCollection().CreateServiceProvider();
        _cancellationToken = new CancellationToken();
        _service = _serviceProvider.GetRequiredService<IPersonService>();
        var db = _serviceProvider.GetRequiredService<AirMasteraDbContext>();
        db.MigrateAndReloadTypes();
    }

    [Theory(DisplayName = "Добавляем запись в БД")]
    [MemberData(nameof(TestHelper.CreatePersonParameters), MemberType = typeof(TestHelper))]
    public async Task CreateAndSavePerson(CreatePersonRequest expectedCreatePerson)
    {
        //Act
        await _service.CreatePersonAsync(expectedCreatePerson, _cancellationToken);
        var actual = _service.GetPersonAsync(expectedCreatePerson.Id, _cancellationToken).Result;

        //Assert
        actual.FullName.Should().Be(expectedCreatePerson.FullName);
    }

    [Theory(DisplayName = "Обновляем Персону в БД")]
    [MemberData(nameof(TestHelper.UpdatePersonParameters), MemberType = typeof(TestHelper))]
    public async Task UpdateAndSavePerson(UpdatePersonRequest expectedUpdatePerson)
    {
        //Arrange
        expectedUpdatePerson.Id = Guid.Parse("b41acc31-6a42-4fda-ad4f-47296e0f0e4f");

        //Act
        await _service.UpdatePersonAsync(expectedUpdatePerson, _cancellationToken);
        var actual = _service.GetPersonAsync(expectedUpdatePerson.Id, _cancellationToken).Result;

        //Assert
        actual.FullName.Should().Be(expectedUpdatePerson.FullName);
    }

    [Theory(DisplayName = "Обновляем или сохраняем авто в БД")]
    [MemberData(nameof(TestHelper.CreateCarParameters), MemberType = typeof(TestHelper))]
    public async Task UpdateAndSaveCar(UpdatePersonRequest expectedUpdatePerson)
    {
        //Arrange
        var personId = Guid.Parse("b41acc31-6a42-4fda-ad4f-47296e0f0e4f");

        expectedUpdatePerson.Id = personId;

        //Act
        await _service.UpdatePersonAsync(expectedUpdatePerson, _cancellationToken);
        var actual = _service.GetPersonAsync(expectedUpdatePerson.Id, _cancellationToken).Result;

        //Assert
        actual.FullName.Should().Be(expectedUpdatePerson.FullName);
    }

    [Theory(DisplayName = "Обновляем или сохраняем ремонт для авто в БД")]
    [MemberData(nameof(TestHelper.CreateRepairParameters), MemberType = typeof(TestHelper))]
    public async Task UpdateAndSaveRepair(UpdateCarRequest expectedUpdatePerson)
    {
        //Arrange
        var carId = Guid.Parse("23766d30-3d14-45dc-9168-1ad5316c1356");

        if (expectedUpdatePerson.Repair != null)
        {
            expectedUpdatePerson.Repair.CarId = carId;
            expectedUpdatePerson.Repair.Id = Guid.Parse("23766d30-3d14-45dc-9168-1ad5316c1357");
        }

        expectedUpdatePerson.Id = carId;

        //Act
        await _service.UpdateCarAsync(expectedUpdatePerson, _cancellationToken);
        var actual = _service.GetCarAsync(expectedUpdatePerson.Id, _cancellationToken).Result;

        //Assert
        actual.Model.Should().Be(expectedUpdatePerson.Model);
    }

    [Theory(DisplayName = "Удаляем запись в БД")]
    [MemberData(nameof(TestHelper.CreatePersonParameters), MemberType = typeof(TestHelper))]
    public async Task DeletePersonTest(CreatePersonRequest expectedCreatePerson)
    {
        //Arrange
        await _service.CreatePersonAsync(expectedCreatePerson, _cancellationToken);

        //Act
        await _service.DeletePersonAsync(expectedCreatePerson.Id, _cancellationToken);
        var result = () => _service.GetPersonAsync(expectedCreatePerson.Id, _cancellationToken).Result;

        //Assert
        result.Should().Throw<NotFoundException>().WithMessage($"Сущность c id: {expectedCreatePerson.Id} не найдена в базе данных...");
    }

    [Theory(DisplayName = "Получаем запись из БД")]
    [MemberData(nameof(TestHelper.CreatePersonParameters), MemberType = typeof(TestHelper))]
    public async void GetPerson(CreatePersonRequest expectedCreatePerson)
    {
        //Act
        await _service.CreatePersonAsync(expectedCreatePerson, _cancellationToken);
        var actual = _service.GetPersonAsync(expectedCreatePerson.Id, _cancellationToken).Result;
        //Assert
        actual.FullName.Should().NotBeEmpty();
    }

    public void Dispose()
    {
        //var db = _serviceProvider.GetRequiredService<AirMasteraDbContext>();
        //db.Database.EnsureDeleted();
    }
}