using System.Collections.Generic;
using LiveCoding.Api.Controllers;
using LiveCoding.Domain;
using LiveCoding.Domain.Ports;

namespace LiveCoding.Tests;

public class InMemoryProvideBooking : ISaveBooking, IBookingQueryRepository
{
    private readonly List<Booking> bookings = new();
    
    public void Save(Booking booking)
    {
        if (booking.GetType() != typeof(BookingNotFound))
        {
            bookings.Add(booking);
        }
    }

    public IEnumerable<Booking> GetUpcomingBookings()
    {
        return bookings;
    }
}