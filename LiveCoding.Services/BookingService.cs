using LiveCoding.Persistence;

namespace LiveCoding.Services
{
    public class BookingService
    {
        private readonly IBarRepository _barRepo;
        private readonly IBoatRepository _boatRepo;
        private readonly IDevRepository _devRepo;
        private readonly IBookingRepository _bookingRepository;

        public BookingService(IBarRepository barRepo,
            IDevRepository devRepo,
            IBoatRepository boatRepo,
            IBookingRepository bookingRepository
        )
        {
            _barRepo = barRepo;
            _boatRepo = boatRepo;
            _devRepo = devRepo;
            _bookingRepository = bookingRepository;
        }

        public bool ReserveBar()
        {
            var bars = _barRepo.Get();
            var boats = _boatRepo.Get();
            var devs = _devRepo.Get().ToList();

            var allBars = GetAllBars(bars, boats);
            var availabilities = GetAvailabilities(devs);

            var bestDate = BestDate.Get(availabilities, devs.Count);
            var booking = Booking.Make(bestDate, allBars);

            if (booking is not BookingNotFound)
            {
                _bookingRepository.Save(new BookingData()
                {
                    Bar = new BarData(booking.Bar.Name, booking.Bar.Capacity, booking.Bar.OpenDays),
                    Date = booking.Date
                });
                return true;
            }

            return false;
        }

        private static IEnumerable<Bar> GetAllBars(IEnumerable<BarData> bars, IEnumerable<BoatData> boats)
        {
            IEnumerable<Bar> allBars = bars.Select(b => new Bar(b.Name, b.Capacity, b.Open, false))
                .Concat(boats.Select(b => new Bar(b.Name, b.MaxPeople, Enum.GetValues<DayOfWeek>(), true)));
            return allBars;
        }

        private List<DevAvailability> GetAvailabilities(List<DevData> devs)
        {
            var availabilities = new List<DevAvailability>();
            foreach (var date in devs.SelectMany(dev => dev.OnSite))
            {
                var devAvailability = availabilities.FirstOrDefault(availability => availability.Date == date);
                if (devAvailability == null)
                {
                    availabilities.Add(new DevAvailability(date, 1));
                }
                else
                {
                    var numberOfPeople = devAvailability.NumberOfPeople + 1;
                    availabilities.Remove(devAvailability);
                    availabilities.Add(new DevAvailability(date, numberOfPeople));
                }
            }
            return availabilities;
        }
    }
}