namespace LiveCoding.Persistence;

public interface ITransactionRepository
{
    IEnumerable<TransactionData> Get(DateTime dateTime);
}