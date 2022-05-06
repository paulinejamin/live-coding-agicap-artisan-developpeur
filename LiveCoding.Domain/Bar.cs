namespace LiveCoding.Domain;

public record Bar(BarName Name, Capacity Capacity, DayOfWeek[] OpenDays, bool IsFavorite)
{
    public bool IsBookable(DateTime date, int capacityRequested) =>
        IsOpen(date) && HasEnoughCapacity(capacityRequested);

    private bool HasEnoughCapacity(int capacityRequested) => Capacity >= capacityRequested;

    private bool IsOpen(DateTime date) => OpenDays.Contains(date.DayOfWeek);

    public void BookBar(DateTime date)
    {
        Console.WriteLine("Bar booked: " + Name + " at " + date);
    }
}