using System;
using System.Collections.Generic;
using System.Linq;
using LiveCoding.Persistence;

namespace LiveCoding.Tests;

public class FakeBookingRepository : IBookingRepository
{
    private readonly List<BookingData> bookings = new();

    public IEnumerable<BookingData> GetUpcomingBookings()
    {
        return bookings;
    }

    public BookingData GetUpcomingBooking(DateTime date)
    {
        return bookings.First(r => r.Date == date);
    }

    public void Save(BookingData booking)
    {
        this.bookings.Add(booking);
    }
}