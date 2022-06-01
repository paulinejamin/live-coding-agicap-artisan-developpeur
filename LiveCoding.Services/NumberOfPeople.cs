namespace LiveCoding.Services;

public record NumberOfPeople(int Value)
{
    public static implicit operator int(NumberOfPeople numberOfPeople) => numberOfPeople.Value;
    public static implicit operator NumberOfPeople(int value) => new(value);
}