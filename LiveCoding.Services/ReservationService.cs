using LiveCoding.Persistence;

namespace LiveCoding.Services
{
    public class ReservationService
    {
        private readonly IBarRepository barRepo;
        private readonly IDevRepository devRepo;
        private readonly FakeBoatRepository boatRepository;

        public ReservationService(IBarRepository barRepo, IDevRepository devRepo, FakeBoatRepository boatRepository)
        {
            this.barRepo = barRepo;
            this.devRepo = devRepo;
            this.boatRepository = boatRepository;
        }

        public Reservation? ReserveBar()
        {
            var bars = barRepo.Get();
            var devs = devRepo.Get();
            var boats = boatRepository.Get();

            Dictionary<DateTime, int> dictionary = new Dictionary<DateTime, int>();
            foreach (var devData in devs)
            {
                foreach (var date in devData.OnSite)
                {
                    if (dictionary.ContainsKey(date))
                    {
                        dictionary[date]++;
                    }
                    else
                    {
                        dictionary.Add(date, 1);
                    }
                }
            }

            var max = dictionary.Values.Max();

            if (max <= devs.Count() * 0.6)
            {
                return null;
            }

            var dateTime = dictionary.First(kv => kv.Value == max).Key;


            foreach (var boatData in boats)
            {
                BookBoat(boatData, dateTime);
                return new Reservation(dateTime, new BarName(boatData.Name));
            }

            foreach (var barData in bars)
            {
                if (barData.Capacity >= max && barData.Open.Contains(dateTime.DayOfWeek))
                {
                    BookBar(barData, dateTime);
                    return new Reservation(dateTime, new BarName(barData.Name));
                }
            }

            return null;
        }

        private void BookBoat(BoatData boatData, DateTime dateTime)
        {
            // TODO send an email ? 
            Console.WriteLine("Boat booked: " + boatData.Name + " at " + dateTime);
        }

        private void BookBar(BarData barData, DateTime dateTime)
        {
            // TODO send an email ? 
            Console.WriteLine("Bar booked: " + barData.Name + " at " + dateTime);
        }
    }

    public class Reservation
    {
        public Reservation(DateTime date, BarName barName)
        {
            BarName = barName;
            Date = date;
        }

        public DateTime Date { get; set; }
        public BarName BarName { get; set; }
    }

    public record BarName(string Value);
}