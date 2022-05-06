using System;
using LiveCoding.Services;
using NFluent;
using Xunit;

namespace LiveCoding.Tests
{
    public class UnitTest1
    {
        // we need to do tests at a higher level or we will have to refactor them along the way
        [Theory]
        [InlineData(new[] { 10, 15 }, 25)]
        public void Test1(int[] transactionAmounts, int expectedCashflow)
        {
            var service = new CashflowService();

            var result = service.ComputeCashflow(DateTime.Today);

            Check.That(result).IsEqualTo(expectedCashflow);
        }
    }
}