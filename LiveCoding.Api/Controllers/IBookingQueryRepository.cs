using LiveCoding.Domain;

namespace LiveCoding.Api.Controllers
{
    public interface IBookingQueryRepository
    {
        IEnumerable<Booking> GetUpcomingBookings();
    }
}