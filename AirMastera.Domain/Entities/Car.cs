namespace AirMastera.Domain;

public class Car
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Model { get; set; }
    public string Number { get; set; }
    public Uri Avatar { get; set; }
    private readonly List<Repair> _repairs;

    public Car(string name, string model,string number, Uri avatar)
    {
        SetName(name);
        SetModel(model);
        SetNumber(number);
        SetAvatar(avatar);
    }

    private void SetName(string name)
    {
        Name = name;
    }

    private void SetModel(string model)
    {
        Model = model;
    }

    private void SetNumber(string number)
    {
        Number = number;
    }

    private void SetAvatar(Uri avatar)
    {
        Avatar = avatar;
    }
}