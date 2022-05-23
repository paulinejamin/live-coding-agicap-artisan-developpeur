using LiveCoding.Domain;

namespace LiveCoding.Services;

public interface IReservationRepository
{
    Reservation GetUpcomingReservations(DateTime date);
    void Save(Reservation reservation);
}