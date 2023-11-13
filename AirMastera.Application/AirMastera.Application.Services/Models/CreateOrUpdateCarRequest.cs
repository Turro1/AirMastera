using System.Text.Json.Serialization;

namespace AirMastera.Application.Services.Models;

public class CreateOrUpdateCarRequest
{
    [JsonIgnore]
    public Guid? Id { get; set; }

    public string Name { get; set; }
    public string Model { get; set; }
    public string Number { get; set; }
    public Uri Avatar { get; set; }

    [JsonIgnore]
    public CreateOrUpdateRepairRequest? Repair { get; set; }
}