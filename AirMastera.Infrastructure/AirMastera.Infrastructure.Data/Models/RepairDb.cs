﻿namespace AirMastera.Infrastructure.Data.Models;

public class RepairDb
{
    public Guid Id { get; }
    public string PartName { get; set; }
    public string PartType { get; set; }

    public string Price { get; set; }
    public DateTime AppointmentDate { get; set; }
    public DateTime AppointmentTime { get; set; }

    /// <summary>
    /// Внешний ключ Pesron
    /// </summary>
    public Guid CarDbId { get; set; }

    /// <summary>
    /// Навигационное свойство
    /// </summary>
    public CarDb? CarDb { get; set; }
}