using System.Text.Json.Serialization;

namespace AirMastera.Application.Services.Models;

public class UpdatePersonRequest
{
    [JsonIgnore]
    public Guid Id { get; set; }

    public string FullName { get; set; }

    public string Phone { get; set; }

    public SaveCarRequest? Car { get; set; }
}