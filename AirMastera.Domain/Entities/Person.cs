using System.Collections.ObjectModel;
using System.Security.AccessControl;

namespace AirMastera.Domain;

public class Person
{
    public string FullName { get; private set; }
    public string Phone { get; private set; }
    private readonly List<Car> _cars = new ();
    public ReadOnlyCollection<Car> WorkExperiences => _cars.AsReadOnly();

    public Person(string fullName, string phone)
    {
        SetFullName(fullName);
        SetPhone(phone);
    }

    private void SetFullName(string fullName)
    {
        FullName = fullName;
    }

    private void SetPhone(string phone)
    {
        Phone = phone;
    }
}