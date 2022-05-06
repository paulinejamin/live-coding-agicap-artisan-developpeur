namespace LiveCoding.Domain;

public record BarName(string Value)
{
    public static implicit operator string(BarName barName) => barName.Value;
    public static implicit operator BarName(string value) => new(value);
}