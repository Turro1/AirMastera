namespace AirMastera.Application.Services.Models;

public class CarDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Model { get; set; }
    public string Number { get; set; }
    public Uri Avatar { get; set; }
    public Guid PersonId { get; set; }
    public PersonDto PersonDto { get; set; }
}