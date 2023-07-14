namespace AirMastera.Infrastructure.Data.Models;

public class PersonDb
{
    public Guid Id { get; set; }
    public string FullName { get; set; }
    public string Phone { get; set; }
    public IEnumerable<CarDb>? Cars { get; set; }
}