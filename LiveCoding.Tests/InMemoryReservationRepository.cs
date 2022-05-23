using System;
using System.Collections.Generic;
using System.Linq;
using LiveCoding.Api.Controllers;
using LiveCoding.Domain;
using LiveCoding.Services;

namespace LiveCoding.Tests;

public class InMemoryReservationRepository : IReservationRepository, IReservationQueryRepository
{
    private readonly List<Reservation> reservations = new List<Reservation>();

    public Reservation GetUpcomingReservations(DateTime date) => reservations.First(r => r.Date == date);

    public void Save(Reservation reservation) => this.reservations.Add(reservation);

    public IEnumerable<Reservation> GetUpcomingReservations()
    {
        return reservations.Where(r => !r.IsCancelled);
    }
}