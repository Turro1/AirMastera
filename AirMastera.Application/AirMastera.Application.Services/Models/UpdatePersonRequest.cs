namespace AirMastera.Application.Services.Models;

public class UpdatePersonRequest
{
    public Guid Id { get; set; }
    public string FullName { get; set; }

    public string Phone { get; set; }

    public SaveCarRequest? Car { get; set; }
}