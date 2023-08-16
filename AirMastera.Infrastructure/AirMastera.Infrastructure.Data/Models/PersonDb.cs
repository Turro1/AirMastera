using System.ComponentModel.DataAnnotations.Schema;

namespace AirMastera.Infrastructure.Data.Models;

[Table("person")]
public class PersonDb
{
    [Column("id")]
    public Guid Id { get; set; }

    [Column("fullname")]
    public string FullName { get; set; }

    [Column("phone")]
    public string Phone { get; set; }

    public IEnumerable<CarDb>? Cars { get; set; }
}