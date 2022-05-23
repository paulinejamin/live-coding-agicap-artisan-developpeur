using LiveCoding.Domain;
using LiveCoding.Persistence;
using LiveCoding.Services;
using Microsoft.AspNetCore.Mvc;

namespace LiveCoding.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReservationController : ControllerBase
    {
        private readonly ReservationService reservationService;

        public ReservationController(ReservationService reservationService)
        {
            this.reservationService = reservationService;
        }

        [HttpGet]
        public Reservation? Get()
        {
            var reservation = reservationService.ReserveBar();
            return reservation == Reservation.Impossible ? null : reservation;
        }
    }
}