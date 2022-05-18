using System;
using LiveCoding.Api.Controllers;
using LiveCoding.Persistence;
using LiveCoding.Services;
using NFluent;
using Xunit;

namespace LiveCoding.Tests
{
    public class ReservationShould
    {
        [Fact]
        public void Reserve_bar_when_at_least_60_percent_of_devs_are_available()
        {
            var expectedBar = "My bar";
            var barData = new BarData[]
            {
                new BarData()
                {
                    Capacity = 10, Food = false, Name = expectedBar,
                    Open = new[] { DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday }
                }
            };
            var wednesday = new DateTime(2022, 05, 11);
            var thursday = wednesday.AddDays(1);
            var friday = wednesday.AddDays(2);
            var devData = new DevData[]
            {
                new DevData() { Name = "Bob 1", OnSite = new DateTime[] { wednesday, thursday, friday } },
                new DevData() { Name = "Bob 2", OnSite = new DateTime[] { thursday } },
                new DevData() { Name = "Bob 3", OnSite = new DateTime[] { friday } },
                new DevData() { Name = "Bob 4", OnSite = new DateTime[] { wednesday, thursday } },
                new DevData() { Name = "Bob 5", OnSite = new DateTime[] { thursday } },
            };
            var endpoint =
                new ReservationController(new ReservationService(new FakeBarRepository(barData),
                    new FakeDevRepository(devData), new FakeBoatRepository(null)));

            var result = endpoint.Get();

            Check.That(result.Date).IsEqualTo(thursday);
            Check.That(result.BarName.Value).IsEqualTo(expectedBar);
        }

        [Fact]
        public void Do_not_reserve_bar_when_only_50_percent_of_devs_are_available()
        {
            var expectedBar = "My bar";
            var barData = new BarData[]
            {
                new BarData()
                {
                    Capacity = 10, Food = false, Name = expectedBar,
                    Open = new[] { DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday }
                }
            };
            var wednesday = new DateTime(2022, 05, 11);
            var thursday = wednesday.AddDays(1);
            var friday = wednesday.AddDays(2);
            var devData = new DevData[]
            {
                new DevData() { Name = "Bob 1", OnSite = new DateTime[] { wednesday, friday } },
                new DevData() { Name = "Bob 2", OnSite = new DateTime[] { thursday } },
                new DevData() { Name = "Bob 3", OnSite = new DateTime[] { friday } },
                new DevData() { Name = "Bob 4", OnSite = new DateTime[] { wednesday } },
                new DevData() { Name = "Bob 5", OnSite = new DateTime[] { thursday } },
            };
            var endpoint =
                new ReservationController(new ReservationService(new FakeBarRepository(barData),
                    new FakeDevRepository(devData), new FakeBoatRepository(null)));

            var result = endpoint.Get();

            Check.That(result).IsEqualTo(null);
        }

        [Fact]
        public void Reserve_bar_when_it_is_open()
        {
            var expectedBar = "My bar";
            var barData = new BarData[]
            {
                new BarData() { Capacity = 10, Food = false, Name = expectedBar, Open = new[] { DayOfWeek.Thursday } },
                new BarData() { Capacity = 20, Food = false, Name = "Le Sirius", Open = new[] { DayOfWeek.Friday } }
            };
            var thursday = new DateTime(2022, 05, 12);
            var devData = new DevData[]
            {
                new DevData() { Name = "Bob", OnSite = new DateTime[] { thursday } },
                new DevData() { Name = "Alice", OnSite = new DateTime[] { thursday } }
            };
            var endpoint = new ReservationController(new ReservationService(new FakeBarRepository(barData),
                new FakeDevRepository(devData), new FakeBoatRepository(null)));

            var result = endpoint.Get();

            Check.That(result.Date).IsEqualTo(thursday);
            Check.That(result.BarName.Value).IsEqualTo(expectedBar);
        }

        [Fact]
        public void Do_not_reserve_bar_when_it_is_closed()
        {
            var expectedBar = "My bar";
            var barData = new BarData[]
            {
                new BarData() { Capacity = 10, Food = false, Name = expectedBar, Open = new[] { DayOfWeek.Thursday } },
                new BarData() { Capacity = 20, Food = false, Name = "Le Sirius", Open = new[] { DayOfWeek.Friday } }
            };
            var wednesday = new DateTime(2022, 05, 11);
            var devData = new DevData[]
            {
                new DevData() { Name = "Bob", OnSite = new DateTime[] { wednesday } },
                new DevData() { Name = "Alice", OnSite = new DateTime[] { wednesday } }
            };
            var endpoint = new ReservationController(new ReservationService(new FakeBarRepository(barData),
                new FakeDevRepository(devData), new FakeBoatRepository(null)));

            var result = endpoint.Get();

            Check.That(result).IsEqualTo(null);
        }

        [Fact]
        public void Choose_bar_that_has_enough_space()
        {
            var expectedBar = "My bar";
            var barData = new BarData[]
            {
                new BarData()
                {
                    Capacity = 3, Food = false, Name = expectedBar,
                    Open = new[] { DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday }
                }
            };
            var wednesday = new DateTime(2022, 05, 11);
            var friday = wednesday.AddDays(2);
            var devData = new DevData[]
            {
                new DevData() { Name = "Bob 1", OnSite = new DateTime[] { wednesday, friday } },
                new DevData() { Name = "Bob 2", OnSite = new DateTime[] { wednesday } },
                new DevData() { Name = "Bob 3", OnSite = new DateTime[] { wednesday } },
                new DevData() { Name = "Bob 4", OnSite = new DateTime[] { wednesday } },
            };
            var endpoint = new ReservationController(new ReservationService(new FakeBarRepository(barData),
                new FakeDevRepository(devData), new FakeBoatRepository(null)));

            var result = endpoint.Get();

            Check.That(result).IsEqualTo(null);
        }

        [Fact]
        public void Choose_boat_over_bar_when_available()
        {
            var barData = new BarData[]
            {
                new BarData()
                {
                    Capacity = 3, Food = false, Name = "my bar",
                    Open = new[] { DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday }
                }
            };
            var wednesday = new DateTime(2022, 05, 11);
            var devData = new DevData[]
            {
                new DevData() { Name = "Bob 1", OnSite = new DateTime[] { wednesday } },
                new DevData() { Name = "Bob 2", OnSite = new DateTime[] { wednesday } },
            };
            string boatName = "boaaaat";
            var boatData = new BoatData[]
            {
                new BoatData()
                    { MaxPeople = 3, Name = boatName, OpenFrom = wednesday, OpenUntil = wednesday.AddDays(4) }
            };
            var endpoint = new ReservationController(new ReservationService(new FakeBarRepository(barData),
                new FakeDevRepository(devData), new FakeBoatRepository(boatData)));

            var result = endpoint.Get();

            Check.That(result.Date).IsEqualTo(wednesday);
            Check.That(result.BarName.Value).IsEqualTo(boatName);
        }

        [Fact]
        public void Choose_bar_when_no_boat_available()
        {
        }


        // TODO CHOOSE : this test or third bar type ???
        [Fact]
        public void Do_not_reserve_boat_when_Agicap_devs_fill_more_than_80_percent()
        {
        }


    }
}