using AirMastera.Application.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using AirMastera.Application.Services.Models;
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
    public async void CreateAndSavePerson(CreatePersonRequest expectedCreatePerson)
    {
        //Act
        await _service.CreatePersonAsync(expectedCreatePerson, _cancellationToken);
        var actual = _service.GetPersonAsync(expectedCreatePerson.Id, _cancellationToken).Result;
        //Assert
        actual.FullName.Should().Be(expectedCreatePerson.FullName);
    }

/*[Theory(DisplayName = "Обновляем запись в БД")]
[MemberData(nameof(TestHelper.UpdatePersonParameters), MemberType = typeof(TestHelper))]
public async void UpdateAndSavePerson(CreatePersonRequest expectedCreatePerson, UpdatePersonRequest expectedUpdatePerson)
{
    //Arrange
    expectedUpdatePerson.Id = expectedCreatePerson.Id;
    //Act
    using (var innerScope = _serviceProvider.CreateScope())
    {
        var service = innerScope.ServiceProvider.GetRequiredService<IPersonService>();
        await service.CreatePersonAsync(expectedCreatePerson, _cancellationToken);
    }

    await _service.UpdatePersonAsync(expectedUpdatePerson, _cancellationToken);
    var actual = _service.GetPersonAsync(expectedUpdatePerson.Id, _cancellationToken).Result;
    //Assert
    actual.Surname.Should().Be(expectedUpdatePerson.Surname);
}

[Theory(DisplayName = "Получаем запись из БД")]
[MemberData(nameof(TestHelper.CreatePersonParameters), MemberType = typeof(TestHelper))]
public async void GetPerson(CreatePersonRequest expectedCreatePerson)
{
    //Act
    await _service.CreatePersonAsync(expectedCreatePerson, _cancellationToken);
    var actual = _service.GetPersonAsync(expectedCreatePerson.Id, _cancellationToken).Result;
    //Assert
    actual.Surname.Should().NotBeEmpty();
}*/

    public void Dispose()
    {
        //var db = _serviceProvider.GetRequiredService<AirMasteraDbContext>();
        //db.Database.EnsureDeleted();
    }
}