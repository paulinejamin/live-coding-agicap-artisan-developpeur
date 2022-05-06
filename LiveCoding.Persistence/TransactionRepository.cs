namespace LiveCoding.Persistence;

public class TransactionRepository : ITransactionRepository
{
    public IEnumerable<TransactionData> Get(DateTime dateTime)
    {
        return new[] { new TransactionData(10), new TransactionData(15) };
    }
}