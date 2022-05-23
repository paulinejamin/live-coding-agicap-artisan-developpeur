namespace LiveCoding.Domain;

public record Bar(BarName Name, int Capacity, DayOfWeek[] OpenDays, bool IsFavorite)
{
    public bool HasEnoughCapacity(int numberOfDevsAvailable) => Capacity >= numberOfDevsAvailable;

    public bool IsOpen(DateTime date) => OpenDays.Contains(date.DayOfWeek);

    public void BookBar(DateTime date)
    {
        Console.WriteLine("Bar booked: " + this.Name + " at " + date);
    }

    public static Bar None = new(new BarName(string.Empty), 0, Array.Empty<DayOfWeek>(), false);

    public void SendCancellation(DateTime date)
    {
        Console.WriteLine("Bar booking cancelled: " + this.Name + " at " + date);
    }
}