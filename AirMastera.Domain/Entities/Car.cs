namespace AirMastera.Domain;

public class Car
{
    public string Name { get; set; }
    public string Model { get; set; }
    public string Number { get; set; }
    public Uri Avatar { get; set; }
    private readonly List<Repair> _repairs;
}