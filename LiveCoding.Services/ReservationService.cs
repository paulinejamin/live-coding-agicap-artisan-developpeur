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

            return 0;
        }
    }
}