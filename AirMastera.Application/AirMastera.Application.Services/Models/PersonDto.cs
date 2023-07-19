namespace AirMastera.Application.Services.Models;

public class PersonDto
{
    public Guid Id { get; set; }
    public string FullName { get; set; }
    public string Phone { get; set; }
    public IEnumerable<CarDto> Cars { get; set; }
}