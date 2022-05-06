namespace LiveCoding.Persistence;

public class BarData
{
    public string Name { get; set; }
    public int Capacity { get; set; }
    public bool Food { get; set; }
    public DayOfWeek[] Open { get; set; }
}