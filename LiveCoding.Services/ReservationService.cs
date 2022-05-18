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

            var numberOfDevsAvailable = dictionary.Values.Max();

            if (numberOfDevsAvailable <= devs.Count() * 0.6)
            {
                return null;
            }

            var dateTime = dictionary.First(kv => kv.Value == numberOfDevsAvailable).Key;

            var allBars = bars.Select(bar => new Bar(new BarName(bar.Name), bar.Capacity, bar.Open, false))
                .Concat(boats.Select(boat => new Bar(new BarName(boat.Name), boat.MaxPeople, Enum.GetValues<DayOfWeek>(), true)))
                .ToList();
            return Reservation.MakeReservation(allBars, dateTime, numberOfDevsAvailable);
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
        private Reservation(DateTime date, BarName barName)
        {
            BarName = barName;
            Date = date;
        }

        public DateTime Date { get; set; }
        public BarName BarName { get; set; }

        public static Reservation? MakeReservation(List<Bar> bars, DateTime date, int numberOfDevsAvailable)
        {
            foreach (var bar in bars.OrderByDescending(b => b.IsFavorite))
            {
                if (bar.HasEnoughCapacity(numberOfDevsAvailable) && bar.IsOpen(date))
                    return new Reservation(date, bar.BarName);
            }

            return null;
        }
    }

    public record Bar(BarName BarName, int Capacity, DayOfWeek[] OpenDays, bool IsFavorite)
    {
        public bool HasEnoughCapacity(int numberOfDevsAvailable) => Capacity >= numberOfDevsAvailable;

        public bool IsOpen(DateTime date) => OpenDays.Contains(date.DayOfWeek);
    }

    public record BarName(string Value);
}