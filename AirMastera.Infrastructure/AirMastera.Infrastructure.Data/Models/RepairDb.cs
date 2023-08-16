using System.ComponentModel.DataAnnotations.Schema;
using AirMastera.Domain.Entities;

namespace AirMastera.Infrastructure.Data.Models;

[Table("repair")]
public class RepairDb
{
    [Column("id")]
    public Guid Id { get; set; }

    [Column("part_name")]
    public string PartName { get; set; }

    [Column("part_type")]
    public string PartType { get; set; }

    [Column("price")]
    public decimal Price { get; set; }

    [Column("repair_status")]
    public RepairStatus RepairStatus { get; set; }

    [Column("appointment_date")]
    public DateTime AppointmentDate { get; set; }

    [Column("car_id")]
    public Guid CarDbId { get; set; }

    [Column("car")]
    public CarDb? CarDb { get; set; }
}