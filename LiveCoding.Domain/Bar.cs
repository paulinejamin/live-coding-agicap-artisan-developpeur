namespace LiveCoding.Domain;

public record Bar(BarName Name, int Capacity, DayOfWeek[] OpenDays, bool IsFavorite)
{
    public bool HasEnoughCapacity(int numberOfDevsAvailable) => Capacity >= numberOfDevsAvailable;

    public bool IsOpen(DateTime date) => OpenDays.Contains(date.DayOfWeek);

    public void BookBar(DateTime dateTime)
    {
        Console.WriteLine("Bar booked: " + this.Name + " at " + dateTime);
    }
}