using LiveCoding.Domain;
using LiveCoding.Persistence;

namespace LiveCoding.Services
{
    public class ReservationService
    {
        private readonly IBarRepository barRepo;
        private readonly IDevRepository devRepo;
        private readonly FakeBoatRepository boatRepository;
        private readonly IReservationRepository reservationRepository;

        public ReservationService(IBarRepository barRepo, IDevRepository devRepo, FakeBoatRepository boatRepository, IReservationRepository reservationRepository)
        {
            this.barRepo = barRepo;
            this.devRepo = devRepo;
            this.boatRepository = boatRepository;
            this.reservationRepository = reservationRepository;
        }

        public Reservation ReserveBar()
        {
            var indoorBars = barRepo.Get();
            var boats = boatRepository.Get();

            var devs = devRepo.Get();
            var bestDate = BestDate.GetBestDate(devs.Count(),devs.SelectMany(d=>d.OnSite));
            if (bestDate == BestDate.NotFound)
                return Reservation.Impossible;

            var allBars = GetIndoorBars(indoorBars)
                .Concat(GetBoats(boats))
                .ToList();

            var reservation = Reservation.MakeReservation(allBars, bestDate.Date, bestDate.NumberOfDevsAvailable);
            if (reservation == Reservation.Impossible)
                return Reservation.Impossible;

            reservationRepository.Save(reservation);
            return reservation;
        }


        private static IEnumerable<Bar> GetBoats(IEnumerable<BoatData> boats) 
            => boats
                .Select(boat => new Bar(new BarName(boat.Name), boat.MaxPeople, Enum.GetValues<DayOfWeek>(), true));

        private static IEnumerable<Bar> GetIndoorBars(IEnumerable<BarData> indoorBars) =>
            indoorBars
                .Select(bar => new Bar(new BarName(bar.Name), bar.Capacity, bar.Open, false));

        public void Cancel(DateTime date)
        {
            var reservation = reservationRepository.GetUpcomingReservations(date);
            reservation.Cancel();
            reservationRepository.Save(reservation);
        }
    }
}