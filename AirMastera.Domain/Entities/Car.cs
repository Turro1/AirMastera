using System.Collections.ObjectModel;

namespace AirMastera.Domain.Entities;

public class Car
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Model { get; set; }
    public string Number { get; set; }
    public Uri Avatar { get; set; }
    private readonly List<Repair> _repairs = new();
    public ReadOnlyCollection<Repair> Repairs => _repairs.AsReadOnly();

    public Car(Guid id, string name, string model, string number, Uri avatar)
    {
        Id = id;
        SetName(name);
        SetModel(model);
        SetNumber(number);
        SetAvatar(avatar);
    }

    private void SetName(string name)
    {
        Name = name;
    }

    private void SetModel(string model)
    {
        Model = model;
    }

    private void SetNumber(string number)
    {
        Number = number;
    }

    private void SetAvatar(Uri avatar)
    {
        Avatar = avatar;
    }

    public void SaveRepair(Guid id, string partName, string partType, decimal price, DateTime appointmentDate)
    {
        var currentItem = _repairs.FirstOrDefault(x => x.Id == id);
        if (currentItem != null)
        {
            _repairs.Remove(currentItem);
        }

        _repairs.Add(new Repair(id, partName, partType, price, appointmentDate));
    }
}