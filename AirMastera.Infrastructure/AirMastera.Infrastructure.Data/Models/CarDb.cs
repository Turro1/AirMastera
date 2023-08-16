using System.ComponentModel.DataAnnotations.Schema;

namespace AirMastera.Infrastructure.Data.Models;

[Table("car")]
public class CarDb
{
    [Column("id")]
    public Guid Id { get; set; }

    [Column("name")]
    public string Name { get; set; }

    [Column("model")]
    public string Model { get; set; }

    [Column("number")]
    public string Number { get; set; }

    [Column("avatar")]
    public Uri Avatar { get; set; }

    /// <summary>
    /// Внешний ключ Pesron
    /// </summary>
    [Column("person_id")]
    public Guid PersonDbId { get; set; }

    /// <summary>
    /// Навигационное свойство
    /// </summary>
    public PersonDb? PersonDb { get; set; }

    public IEnumerable<RepairDb>? Repairs { get; set; }
}