using LiveCoding.Persistence;

namespace LiveCoding.Services
{
    public class CashflowService
    {
        private readonly ITransactionRepository transactionRepo;

        public CashflowService(ITransactionRepository transactionRepo)
        {
            this.transactionRepo = transactionRepo;
        }

        public int ComputeCashflow(DateTime dateTime)
        {
            var transactions = transactionRepo.Get(dateTime);

            return transactions.Sum(t => t.Amount);
        }
    }
}