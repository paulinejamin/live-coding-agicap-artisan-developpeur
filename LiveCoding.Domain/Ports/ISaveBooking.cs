namespace LiveCoding.Domain.Ports;

public interface ISaveBooking
{
    void Save(Booking booking);
}