using LiveCoding.Persistence;
using LiveCoding.Services;
using Microsoft.AspNetCore.Mvc;

namespace LiveCoding.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly BookingService bookingService;
        private readonly IBookingRepository bookingRepository;

        public BookingController(BookingService bookingService, IBookingRepository bookingRepository)
        {
            this.bookingService = bookingService;
            this.bookingRepository = bookingRepository;
        }
        
        [HttpPut]
        public bool MakeBooking()
        {
            return bookingService.ReserveBar();
        }

        [HttpGet]
        public IEnumerable<BookingData> Get()
        {
            return bookingRepository.GetUpcomingBookings();
        }


        [HttpPost]
        public void Cancel(DateTime date)
        {
            bookingService.Cancel(date);
        }
    }
}