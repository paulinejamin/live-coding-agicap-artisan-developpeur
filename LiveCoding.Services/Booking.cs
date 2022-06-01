namespace LiveCoding.Services;

public class Booking
{
    protected Booking(DateTime date, Bar bar)
    {
        Bar = bar;
        Date = date;
    }

    public DateTime Date { get; }
    public Bar Bar { get; }

    public static Booking Make(BestDate bestDate, IEnumerable<Bar> bars)
    {
        foreach (var bar in bars.OrderByDescending(bar => bar.IsFavorite))
        {
            if (bar.IsBookable(bestDate.Date, bestDate.NumberOfDevsAvailable))
            {
                bar.BookBar(bestDate.Date);
                return new Booking(bestDate.Date, bar);
            }
        }

        return new BookingNotFound();
    }
}