using System;
using LiveCoding.Api.Controllers;
using LiveCoding.Services;
using NFluent;
using Xunit;

namespace LiveCoding.Tests
{
    public class UnitTest1
    {
        [Theory]
        [InlineData(new[] { 10, 15 }, 25)]
        public void Test1(int[] transactionAmounts, int expectedCashflow)
        {
            var service = new CashflowController(new CashflowService());

            var result = service.Get(DateTime.Today);

            Check.That(result).IsEqualTo(expectedCashflow);
        }
    }
}