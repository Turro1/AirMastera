namespace AirMastera.Domain.Entities;

public class Repair
{
    public Guid Id { get; }
    public string PartName { get; set; }
    public string PartType { get; set; }

    public decimal Price { get; set; }
    public DateTime AppointmentDate { get; set; }

    public Repair(Guid id, string partName, string partType, decimal price, DateTime appointmentDate)
    {
        Id = id;
        SetPartName(partName);
        SetPartType(partType);
        SetPrice(price);
        SetAppointmentDate(appointmentDate);
    }

    public void SetPartName(string partName)
    {
        PartName = partName;
    }

    private void SetPartType(string partType)
    {
        PartType = partType;
    }

    private void SetPrice(decimal price)
    {
        Price = price;
    }

    private void SetAppointmentDate(DateTime appointmentDate)
    {
        AppointmentDate = appointmentDate;
    }
}