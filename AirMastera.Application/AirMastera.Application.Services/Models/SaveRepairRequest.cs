namespace AirMastera.Application.Services.Models;

public class SaveRepairRequest
{
    public Guid Id { get; set; }
    public string PartName { get; set; }
    public string PartType { get; set; }
    public decimal Price { get; set; }
    public DateTime AppointmentDate { get; set; }
    public Guid CarId { get; set; }
}