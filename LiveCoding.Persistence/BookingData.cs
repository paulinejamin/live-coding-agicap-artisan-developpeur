namespace LiveCoding.Persistence;

public class BookingData
{
    public DateTime Date { get; set; }
    public BarData Bar { get; set; }
    public bool IsCancelled { get; set; }
}