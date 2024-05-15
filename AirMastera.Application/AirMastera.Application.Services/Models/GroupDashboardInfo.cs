namespace AirMastera.Application.Services.Models;

public class GroupDashboardInfo
{
    public DateTime RepairDate { get; set; }
    public List<DashboardInfo> DashboardInfos { get; set; }
}