using LiveCoding.Persistence;

namespace LiveCoding.Services
{
    public class Bar
    {
        public bool HasEnoughCapacity(BoatData boatData, int maxNumberOfDevs)
        {
            return boatData.MaxPeople >= maxNumberOfDevs;
        }
    }

    public class BookingService
    {
        private readonly IBarRepository _barRepo;
        private readonly IDevRepository _devRepo;
        private readonly IBoatRepository _boatRepo;
        private readonly IBookingRepository _bookingRepository;

        public BookingService(IBarRepository barRepo,
            IDevRepository devRepo,
            IBoatRepository boatRepo,
            IBookingRepository bookingRepository
        )
        {
            _barRepo = barRepo;
            _devRepo = devRepo;
            _boatRepo = boatRepo;
            _bookingRepository = bookingRepository;
        }

        public bool ReserveBar()
        {
            var bars = _barRepo.Get();
            var devs = _devRepo.Get().ToList();
            var boats = _boatRepo.Get();

            var numberOfAvailableDevsByDate = new Dictionary<DateTime, int>();
            foreach (var devData in devs)
            {
                foreach (var date in devData.OnSite)
                {
                    if (numberOfAvailableDevsByDate.ContainsKey(date))
                    {
                        numberOfAvailableDevsByDate[date]++;
                    }
                    else
                    {
                        numberOfAvailableDevsByDate.Add(date, 1);
                    }
                }
            }

            var maxNumberOfDevs = numberOfAvailableDevsByDate.Values.Max();

            if (maxNumberOfDevs <= devs.Count() * 0.6)
            {
                return false;
            }

            var bestDate = numberOfAvailableDevsByDate.First(kv => kv.Value == maxNumberOfDevs).Key;

            foreach (var boatData in boats)
            {
                var bar = new Bar();
                if (bar.HasEnoughCapacity(boatData, maxNumberOfDevs))
                {
                    BookBar(boatData.Name, bestDate);
                    _bookingRepository.Save(new BookingData() { Bar = new BarData(boatData.Name, boatData.MaxPeople, AllDays()), Date = bestDate });
                    return true;
                }
            }

            foreach (var barData in bars)
            {
                if (barData.Capacity >= maxNumberOfDevs && barData.Open.Contains(bestDate.DayOfWeek))
                {
                    BookBar(barData.Name, bestDate);
                    _bookingRepository.Save(new BookingData() { Bar = barData, Date = bestDate });
                    return true;
                }
            }

            return false;
        }

        private static DayOfWeek[] AllDays()
        {
            return Enum.GetValues<DayOfWeek>();
        }

        private void BookBar(string name, DateTime dateTime)
        {
            Console.WriteLine("Bar booked: " + name + " at " + dateTime);
        }
    }
}