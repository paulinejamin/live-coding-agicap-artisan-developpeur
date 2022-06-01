namespace LiveCoding.Persistence;

public record BarData
{
    public string Name { get; set; }
    public int Capacity { get; set; }
    public DayOfWeek[] Open { get; set; }
}