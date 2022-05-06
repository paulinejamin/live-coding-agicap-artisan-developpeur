namespace LiveCoding.Domain;

public record Capacity(int Value)
{
    public static implicit operator int(Capacity capacity) => capacity.Value;
    public static implicit operator Capacity(int value) => new(value);
}