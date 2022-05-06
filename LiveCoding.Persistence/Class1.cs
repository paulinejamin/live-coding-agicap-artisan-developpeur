using System.Transactions;

namespace LiveCoding.Persistence
{
    public class TransactionData
    {
        public TransactionData(int amount)
        {
            Amount = amount;
        }

        public int Amount { get; }
    }
}