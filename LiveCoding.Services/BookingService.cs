using LiveCoding.Persistence;

namespace LiveCoding.Services
{
    public class BookingService
    {
        private readonly IDevRepository _devRepo;
        private readonly IBookingRepository _bookingRepository;
        private readonly IProvideBars _barAdapter;

        public BookingService(IDevRepository devRepo,
            IBookingRepository bookingRepository, 
            IProvideBars barAdapter)
        {
            _devRepo = devRepo;
            _bookingRepository = bookingRepository;
            _barAdapter = barAdapter;
        }

        public bool ReserveBar()
        {
            var devs = _devRepo.Get().ToList();

            var allBars = _barAdapter.GetAllBars();
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