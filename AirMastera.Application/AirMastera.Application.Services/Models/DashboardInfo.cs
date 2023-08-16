using AirMastera.Domain.Entities;
using AirMastera.Infrastructure.Data.Models;

namespace AirMastera.Application.Services.Models;

public class DashboardInfo
{
    public Guid Id { get; set; }
    public Uri Avatar { get; set; }
    public string CarName { get; set; }
    public string CarModel { get; set; }
    public string PhoneNumber { get; set; }
    public string RepairStatus { get; set; }
    public DateTime AppointmentDate { get; set; }
}