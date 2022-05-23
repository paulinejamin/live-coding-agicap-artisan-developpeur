using LiveCoding.Domain;
using LiveCoding.Services;
using Microsoft.AspNetCore.Mvc;

namespace LiveCoding.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReservationController : ControllerBase
    {
        private readonly ReservationService reservationService;
        private IReservationQueryRepository reservationRepository;

        public ReservationController(ReservationService reservationService, IReservationQueryRepository reservationRepository)
        {
            this.reservationService = reservationService;
            this.reservationRepository = reservationRepository;
        }

        [HttpPut]
        public Reservation? MakeReservation()
        {
            var reservation = reservationService.ReserveBar();
            return reservation == Reservation.Impossible ? null : reservation;
        }

        [HttpGet]
        public IEnumerable<Reservation> Get()
        {
            return reservationRepository.GetUpcomingReservations();
        }


        [HttpPost]
        public void Cancel(DateTime date)
        {
            reservationService.Cancel(date);
        }
    }

    public interface IReservationQueryRepository
    {
        IEnumerable<Reservation> GetUpcomingReservations();
    }
}