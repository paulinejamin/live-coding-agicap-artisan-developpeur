using System;
using LiveCoding.Api.Controllers;
using LiveCoding.Services;
using NFluent;
using Xunit;

namespace LiveCoding.Tests
{
    public class CashflowShould
    {
        [Theory]
        [InlineData(new[] { 10, 5 }, 15)]
        [InlineData(new[] { 10, -5 }, 5)]
        public void Have_correct_cashflow(int[] transactionAmounts, int expectedCashflow)
        {
            var service = new CashflowController(new CashflowService(new FakeTransactionRepository(transactionAmounts)));

            var result = service.Get(DateTime.Today);

            Check.That(result).IsEqualTo(expectedCashflow);
        }
    }
}