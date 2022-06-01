using System;
using System.Collections.Generic;
using System.Linq;
using LiveCoding.Persistence;

namespace LiveCoding.Tests;

public class FakeBookingRepository : IBookingRepository
{
    private readonly List<BookingData> _bookings = new();

    public IEnumerable<BookingData> GetUpcomingBookings()
    {
        return _bookings;
    }

    public BookingData GetUpcomingBooking(DateTime date)
    {
        return _bookings.First(r => r.Date == date);
    }

    public void Save(BookingData booking)
    {
        _bookings.Add(booking);
    }
}