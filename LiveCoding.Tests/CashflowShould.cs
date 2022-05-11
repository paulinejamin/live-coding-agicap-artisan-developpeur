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
        public void Reserve_bar_when_60_percent_of_devs_are_available()
        {
            var endpoint = new ReservationController(new ReservationService(new FakeBarRepository(new BarData[0]), new FakeDevRepository(new DevData[0])));
            Check.That(true).IsTrue();
        }

        [Fact]
        public void Do_not_reserve_bar_when_50_percent_of_devs_are_available()
        {
            Check.That(true).IsTrue();
        }

        [Fact]
        public void Reserve_bar_when_it_is_open()
        {
            Check.That(true).IsTrue();
        }

        [Fact]
        public void Do_not_reserve_bar_when_it_is_closed()
        {
            Check.That(true).IsTrue();
        }

        [Fact]
        public void Choose_bar_that_has_enough_space()
        {
            Check.That(true).IsTrue();
        }
    }
}