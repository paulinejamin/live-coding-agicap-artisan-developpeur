using LiveCoding.Domain;
using LiveCoding.Domain.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace LiveCoding.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class BookingController : ControllerBase
{
    private readonly MakeABooking makeABooking;
    private readonly IBookingQueryRepository bookingRepository;

    public BookingController(MakeABooking makeABooking, IBookingQueryRepository bookingRepository)
    {
        this.makeABooking = makeABooking;
        this.bookingRepository = bookingRepository;
    }

    [HttpPut]
    public bool MakeBooking()
    {
        var booking = makeABooking.Make();
        return booking.GetType() != typeof(BookingNotFound);
    }

    [HttpGet]
    public IEnumerable<Booking> Get()
    {
        return bookingRepository.GetUpcomingBookings();
    }
}