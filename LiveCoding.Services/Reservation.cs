namespace LiveCoding.Services;

public class Reservation
{
    private Reservation(DateTime date, BarName barName)
    {
        BarName = barName;
        Date = date;
    }

    public DateTime Date { get; set; }
    public BarName BarName { get; set; }

    public static Reservation Impossible = new Reservation(DateTime.MinValue, new BarName(string.Empty));

    public static Reservation MakeReservation(List<Bar> bars, DateTime date, int numberOfDevsAvailable)
    {
        foreach (var bar in bars.OrderByDescending(b => b.IsFavorite))
        {
            if (bar.HasEnoughCapacity(numberOfDevsAvailable) && bar.IsOpen(date))
            {
                bar.BookBar(date);
                return new Reservation(date, bar.Name);
            }
        }
        return Reservation.Impossible;
    }
}