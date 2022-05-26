using System;
using System.Linq;
using LiveCoding.Api.Controllers;
using LiveCoding.Persistence;
using LiveCoding.Services;
using NFluent;
using Xunit;

namespace LiveCoding.Tests
{
    public class BookingShould
    {
        [Fact]
        public void Reserve_bar_when_at_least_60_percent_of_devs_are_available()
        {
            var expectedBar = "La belle équipe";
            var barData = new[]
            {
                ABar() with { Name = expectedBar, Open = new[] { DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday } }
            };
            var devData = new[]
            {
                new DevData { Name = "Alice", OnSite = new[] { Wednesday, Thursday, Friday } },
                new DevData { Name = "Bob", OnSite = new[] { Thursday } },
                new DevData { Name = "Chad", OnSite = new[] { Friday } },
                new DevData { Name = "Dan", OnSite = new[] { Wednesday, Thursday } },
                new DevData { Name = "Eve", OnSite = new[] { Thursday } },
            };
            var controller = BuildController(barData, devData);

            controller.MakeBooking();

            var result = controller.Get().Single();
            Check.That(result.Date).IsEqualTo(Thursday);
            Check.That(result.Bar.Name).IsEqualTo(expectedBar);
        }

        [Fact]
        public void Do_not_reserve_bar_when_only_50_percent_of_devs_are_available()
        {
            var barData = new[]
            {
                ABar() with { Open = new[] { DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday } }
            };
            var devData = new[]
            {
                new DevData { Name = "Alice", OnSite = new[] { Wednesday, Friday } },
                new DevData { Name = "Bob", OnSite = new[] { Thursday } },
                new DevData { Name = "Chad", OnSite = new[] { Friday } },
                new DevData { Name = "Dan", OnSite = new[] { Wednesday } },
                new DevData { Name = "Eve", OnSite = new[] { Thursday } },
            };

            var controller = BuildController(barData, devData);

            controller.MakeBooking();

            var result = controller.Get();
            Check.That(result).IsEmpty();
        }

        [Fact]
        public void Reserve_bar_when_it_is_open()
        {
            var expectedBar = "La belle équipe";
            var barData = new[]
            {
                ABar() with { Name = expectedBar, Open = new[] { DayOfWeek.Thursday } },
                ABar() with { Name = "Le Sirius", Open = new[] { DayOfWeek.Friday } }
            };
            var devData = new[]
            {
                new DevData { Name = "Bob", OnSite = new[] { Thursday } },
                new DevData { Name = "Alice", OnSite = new[] { Thursday } }
            };

            var controller = BuildController(barData, devData);

            controller.MakeBooking();

            var result = controller.Get().Single();

            Check.That(result.Date).IsEqualTo(Thursday);
            Check.That(result.Bar.Name).IsEqualTo(expectedBar);
        }

        [Fact]
        public void Do_not_reserve_bar_when_it_is_closed()
        {
            var barData = new[]
            {
                ABar() with { Name = "La belle équipe", Open = new[] { DayOfWeek.Thursday } },
                ABar() with { Name = "Le Sirius", Open = new[] { DayOfWeek.Friday } }
            };
            var devData = new[]
            {
                new DevData { Name = "Bob", OnSite = new[] { Wednesday } },
                new DevData { Name = "Alice", OnSite = new[] { Wednesday } }
            };

            var controller = BuildController(barData, devData);

            controller.MakeBooking();

            var result = controller.Get();

            Check.That(result).IsEmpty();
        }

        [Fact]
        public void Choose_bar_that_has_enough_space()
        {
            var barData = new[]
            {
                ABar() with { Capacity = 3 }
            };
            var devData = new[]
            {
                new DevData { Name = "Bob", OnSite = new[] { Wednesday, Friday } },
                new DevData { Name = "Chad", OnSite = new[] { Wednesday } },
                new DevData { Name = "Dan", OnSite = new[] { Wednesday } },
                new DevData { Name = "Eve", OnSite = new[] { Wednesday } },
            };

            var controller = BuildController(barData, devData);

            controller.MakeBooking();

            var result = controller.Get();

            Check.That(result).IsEmpty();
        }

        private static BookingController BuildController(BarData[] barData, DevData[] devData)
        {
            var bookingRepository = new FakeBookingRepository();
            return new BookingController(new BookingService(new FakeBarRepository(barData),
                new FakeDevRepository(devData), bookingRepository), bookingRepository);
        }

        private static BarData ABar() => new()
        {
            Name = "Wallace", Capacity = 10, Food = false,
            Open = new[] { DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday }
        };


        private static readonly DateTime Wednesday = new(2022, 05, 11);
        private static readonly DateTime Thursday = Wednesday.AddDays(1);
        private static readonly DateTime Friday = Wednesday.AddDays(2);

    }
}
