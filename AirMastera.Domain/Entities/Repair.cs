namespace AirMastera.Domain;

public class Repair
{
    public Guid Id { get; }
    public string PartName { get; set; }
    public string PartType { get; set; }

    public string Price { get; set; }
    public DateTime AppointmentDate { get; set; }
    public DateTime AppointmentTime { get; set; }

    public Repair(string partName, string partType, string price, DateTime appointmentDate, DateTime appointmentTime)
    {
        SetPartName(partName);
        SetPartType(partType);
        SetPrice(price);
        SetAppointmentDate(appointmentDate);
        SetAppointmentTime(appointmentTime);
    }

    public void SetPartName(string partName)
    {
        PartName = partName;
    }

    private void SetPartType(string partType)
    {
        PartType = partType;
    }

    private void SetPrice(string price)
    {
        Price = price;
    }

    private void SetAppointmentDate(DateTime appointmentDate)
    {
        AppointmentDate = appointmentDate;
    }

    private void SetAppointmentTime(DateTime appointmentTime)
    {
        AppointmentTime = appointmentTime;
    }
}