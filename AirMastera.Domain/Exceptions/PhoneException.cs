namespace AirMastera.Domain.Exceptions;

public class PhoneException : Exception
{
    public PhoneException(string message)
        : base(message)
    {
    }
}