﻿namespace AirMastera.Infrastructure.Data.Models;

public class CarDb
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Model { get; set; }
    public string Number { get; set; }
    public Uri Avatar { get; set; }

    /// <summary>
    /// Внешний ключ Pesron
    /// </summary>
    public Guid PersonDbId { get; set; }

    /// <summary>
    /// Навигационное свойство
    /// </summary>
    public PersonDb? PersonDb { get; set; }

    public IEnumerable<RepairDb>? Repairs { get; set; }
}