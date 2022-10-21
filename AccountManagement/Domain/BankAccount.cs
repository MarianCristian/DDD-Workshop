using Common.Entities;

namespace AccountManagement.Domain
{
    public class BankAccount : AggregateRoot
    {
        public Amount AvailbleBalance { get; set; }
        public List<Transaction> Transactions { get; private set; }
        public BankAccount()
        {
            AvailbleBalance = new Amount(100, "RON");
        }
        public void AddTransaction(Transaction transaction)
        {
            if (AvailbleBalance.Value < transaction.Amount.Value)
                throw new InvalidOperationException();

            Transactions.Add(transaction);
            AvailbleBalance = new Amount(AvailbleBalance.Value - transaction.Amount.Value, AvailbleBalance.Currency);
        }
    }
}
