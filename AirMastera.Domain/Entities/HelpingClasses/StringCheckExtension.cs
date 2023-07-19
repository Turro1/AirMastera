namespace AirMastera.Domain.Entities.HelpingClasses;

public static class StringCheckExtension
{
    public static void StringLenght(this string str, string fieldName, int minLenght, int maxLenght)
    {
        if (str.Length < minLenght || str.Length > maxLenght)
        {
            throw new ArgumentException(
                $"Длина поля {fieldName} не может быть меньше {minLenght} и больше {maxLenght}");
        }
    }
}