namespace AirMastera.Application.Services.Models;

public class CreatePersonRequest
{
    public Guid Id { get; set; }
    public string FullName { get; set; }
    public string Phone { get; set; }
}