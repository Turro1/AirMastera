using AirMastera.Domain;
using AirMastera.Domain.Entities;
using AirMastera.Domain.Exceptions;

namespace AirMastera.Tests.Unit;

public class PersonTests
{
    [Fact]
    public void PhoneCheck()
    {
        //a
        var phone = "+33377750032";
        //a
        var action = () => new Person(Guid.NewGuid(),"Турчанинов Роман", phone);
        //a
        Assert.Throws<PhoneException>(action);
    }

    [Fact]
    public void FullNameCheck()
    {
        //a
        var fullName = "Турчанинов Роман ееееееееееееее";
        //a
        var action = () => new Person(Guid.NewGuid(),fullName ,"+37377750032");
        //a
        Assert.Throws<ArgumentException>(action);
    }
}