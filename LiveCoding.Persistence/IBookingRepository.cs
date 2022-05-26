namespace LiveCoding.Persistence;

public 
    interface IBookingRepository
{
    IEnumerable<BookingData> GetUpcomingBookings();
    BookingData GetUpcomingBooking(DateTime date);
    void Save(BookingData booking);
}