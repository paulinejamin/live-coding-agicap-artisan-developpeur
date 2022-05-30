using LiveCoding.Persistence;

namespace LiveCoding.Services
{
    public class BookingService
    {
        private readonly IBarRepository barRepo;
        private readonly IDevRepository devRepo;
        private readonly IBoatRepository boatRepo;
        private readonly IBookingRepository bookingRepository;

        public BookingService(IBarRepository barRepo,
            IDevRepository devRepo,
            IBoatRepository boatRepo,
            IBookingRepository bookingRepository)
        {
            this.barRepo = barRepo;
            this.devRepo = devRepo;
            this.boatRepo = boatRepo;
            this.bookingRepository = bookingRepository;
        }

        public bool ReserveBar()
        {
            var bars = barRepo.Get();
            var devs = devRepo.Get();

            var dictionary = new Dictionary<DateTime, int>();
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
                return false;
            }

            var dateTime = dictionary.First(kv => kv.Value == max).Key;

            foreach (var barData in bars)
            {
                if (barData.Capacity >= max && barData.Open.Contains(dateTime.DayOfWeek))
                {
                    BookBar(barData, dateTime);
                    bookingRepository.Save(new BookingData() { Bar = barData, Date = dateTime });
                    return true;
                }
            }

            return false;
        }

        private void BookBar(BarData barData, DateTime dateTime)
        {
            Console.WriteLine("Bar booked: " + barData.Name + " at " + dateTime);
        }

        public void Cancel(DateTime date)
        {
            var booking = bookingRepository.GetUpcomingBooking(date);
            this.SendBarCancellation(booking.Bar, booking.Date);
            booking.IsCancelled = true;
            bookingRepository.Save(booking);
        }

        private void SendBarCancellation(BarData bar, DateTime? date)
        {
            Console.WriteLine("Bar booking cancelled: " + bar + " at " + date);
        }
    }
}