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

        public Tuple<DateTime?, BarData?> ReserveBar()
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
                return new Tuple<DateTime?, BarData?>(null, null);
            }

            var dateTime = dictionary.First(kv => kv.Value == max).Key;


            foreach (var boatData in boats)
            {
                BookBoat(boatData, dateTime);
                BarData barBoat = new()
                {
                    Name = boatData.Name,
                    Capacity = boatData.MaxPeople
                };
                return new Tuple<DateTime?, BarData?>(dateTime, barBoat);
            }

            foreach (var barData in bars)
            {
                if (barData.Capacity >= max && barData.Open.Contains(dateTime.DayOfWeek))
                {
                    BookBar(barData, dateTime);
                    return new Tuple<DateTime?, BarData?>(dateTime, barData);
                }
            }

            return new Tuple<DateTime?, BarData?>(null, null);
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
}