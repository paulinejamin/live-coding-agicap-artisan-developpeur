namespace LiveCoding.Persistence;

public interface IBookingRepository
{
    IEnumerable<BookingData> GetUpcomingBookings();
    void Save(BookingData booking);
}