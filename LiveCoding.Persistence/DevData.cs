namespace LiveCoding.Persistence;

public record DevData
{
    public string Name { get; set; }
    public DateTime[] OnSite { get; set; }
}