using LiveCoding.Persistence;
using LiveCoding.Services;
using Microsoft.AspNetCore.Mvc;

namespace LiveCoding.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly BookingService _bookingService;
        private readonly IBookingRepository _bookingRepository;

        public BookingController(BookingService bookingService, IBookingRepository bookingRepository)
        {
            _bookingService = bookingService;
            _bookingRepository = bookingRepository;
        }
        
        [HttpPut]
        public bool MakeBooking()
        {
            return _bookingService.ReserveBar();
        }

        [HttpGet]
        public IEnumerable<BookingData> Get()
        {
            return _bookingRepository.GetUpcomingBookings();
        }
    }
}