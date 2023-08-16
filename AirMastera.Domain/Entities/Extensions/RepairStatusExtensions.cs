namespace AirMastera.Domain.Entities.Extensions;

public static class RepairStatusExtensions
{
    public static string ToText(this RepairStatus status)
    {
        switch (status)
        {
            case RepairStatus.Created: return "Created";
            case RepairStatus.WorkInProcess: return "Work In Process";
            case RepairStatus.Failed: return "Failed";
            case RepairStatus.Finished: return "Finished";
            default: return "Unknown";
        }
    }
}