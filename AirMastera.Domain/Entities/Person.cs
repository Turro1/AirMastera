﻿using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using AirMastera.Domain.Entities.Extensions;
using AirMastera.Domain.Exceptions;

namespace AirMastera.Domain.Entities;

public class Person
{
    public Guid Id { get; private set; }
    public string FullName { get; private set; }
    public string Phone { get; private set; }
    private readonly List<Car> _cars = new();
    public ReadOnlyCollection<Car> Cars => _cars.AsReadOnly();

    public Person(Guid id, string fullName, string phone)
    {
        Id = id;
        SetFullName(fullName);
        SetPhone(phone);
    }

    private void SetFullName(string fullName)
    {
        fullName.StringLenght(nameof(fullName), 2, 30);
        FullName = fullName;
    }

    private void SetPhone(string phone)
    {
        if (!Regex.IsMatch(phone, @"^((\+(373)77)+([4-9]){1}([0-9]){5})$"))
        {
            throw new PhoneException($"Некорректное поле {nameof(phone)}...");
        }

        Phone = phone;
    }

    public void SaveCar(Guid id, string name, string model, string number, Uri avatar)
    {
        var currentItem = _cars.FirstOrDefault(x => x.Id == id);
        if (currentItem != null)
        {
            _cars.Remove(currentItem);
        }

        _cars.Add(new Car(id, name,model, number, avatar));
    }
}