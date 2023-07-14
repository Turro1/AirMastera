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
    private readonly CancellationToken _cancellationToken;
    private readonly IPersonService _service;

    public IntegrationsTests()
    {
        var serviceProvider = new MyServiceCollection().CreateServiceProvider();
        _cancellationToken = new CancellationToken();
        _service = serviceProvider.GetRequiredService<IPersonService>();
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

    [Theory(DisplayName = "Обновляем запись в БД")]
    [MemberData(nameof(TestHelper.CreateAndUpdatePersonParameters), MemberType = typeof(TestHelper))]
    public async Task UpdateAndSavePerson(CreatePersonRequest expectedCreatePerson, UpdatePersonRequest expectedUpdatePerson)
    {
        //Arrange
        await _service.CreatePersonAsync(expectedCreatePerson, _cancellationToken);
        expectedUpdatePerson.Id = expectedCreatePerson.Id;

        //Act
        await _service.UpdatePersonAsync(expectedUpdatePerson, _cancellationToken);
        var actual = _service.GetPersonAsync(expectedUpdatePerson.Id, _cancellationToken).Result;

        //Assert
        actual.FullName.Should().Be(expectedUpdatePerson.FullName);
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

/*[Theory(DisplayName = "Получаем запись из БД")]
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