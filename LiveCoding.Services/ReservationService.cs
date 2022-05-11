using LiveCoding.Persistence;

namespace LiveCoding.Services
{
    public class ReservationService
    {
        private readonly IBarRepository barRepo;
        private readonly IDevRepository devRepo;

        public ReservationService(IBarRepository barRepo, IDevRepository devRepo)
        {
            this.barRepo = barRepo;
            this.devRepo = devRepo;
        }

        public int ReserveBar(DateTime dateTime)
        {
            var bars = barRepo.Get();
            var devs = devRepo.Get();

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

            var bar = new BarData();
            foreach (var barData in bars)
            {
                if (barData.Capacity <= max * 0.6)
                {
                    bar = barData;
                }
            }

            BookBar(bar);

            return max;
        }

        private void BookBar(BarData barData)
        {
            // TODO send an email ? 
            throw new NotImplementedException();
        }
    }
}