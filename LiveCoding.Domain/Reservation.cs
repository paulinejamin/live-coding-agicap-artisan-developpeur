namespace LiveCoding.Domain;

public class Reservation
{
    private Reservation(DateTime date, Bar bar)
    {
        Bar = bar;
        Date = date;
    }

    public DateTime Date { get; set; }
    public Bar Bar { get; set; }

    public static Reservation Impossible = new Reservation(DateTime.MinValue, Bar.None);

    public bool IsCancelled { get; set; }

    public static Reservation MakeReservation(List<Bar> bars, DateTime date, int numberOfDevsAvailable)
    {
        foreach (var bar in bars.OrderByDescending(b => b.IsFavorite))
        {
            if (bar.HasEnoughCapacity(numberOfDevsAvailable) && bar.IsOpen(date))
            {
                bar.BookBar(date);
                return new Reservation(date, bar);
            }
        }
        return Reservation.Impossible;
    }

    public void Cancel()
    {
        this.Bar.SendCancellation(this.Date);
        IsCancelled = true;
    }
}