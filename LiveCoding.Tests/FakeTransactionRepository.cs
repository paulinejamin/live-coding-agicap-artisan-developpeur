using System;
using System.Collections.Generic;
using System.Linq;
using LiveCoding.Persistence;

namespace LiveCoding.Tests;

public class FakeTransactionRepository : ITransactionRepository
{
    private readonly IEnumerable<TransactionData> transactions;

    public FakeTransactionRepository(int[] transactionAmounts)
    {
        transactions = transactionAmounts.Select(a => new TransactionData(a));
    }

    public IEnumerable<TransactionData> Get(DateTime dateTime)
    {
        return transactions;
    }
}