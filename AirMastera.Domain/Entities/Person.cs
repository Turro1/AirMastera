﻿using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using AirMastera.Domain.Exceptions;
using AirMastera.Domain.HelpingClasses;

namespace AirMastera.Domain;

public class Person
{
    public string FullName { get; private set; }
    public string Phone { get; private set; }
    private readonly List<Car> _cars = new();
    public ReadOnlyCollection<Car> WorkExperiences => _cars.AsReadOnly();

    public Person(string fullName, string phone)
    {
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
}