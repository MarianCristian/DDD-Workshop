using AccountManagement.DTO;
using AccountManagement.Events;
using Common.Entities;

namespace AccountManagement.Domain
{
    public class BankAccount : AggregateRoot
    {
        public Amount AvailbleBalance { get; set; }
        public string OwnerName { get; set; }
        public List<Transaction> Transactions { get; private set; } = new List<Transaction>();


        public BankAccount()
        {
            AvailbleBalance = new Amount(100, "RON");

            AddHandler<BankAccountRegistered>(When);
            AddHandler<TransactionInitiated>(When);
        }


        public void AddTransaction(Transaction transaction)
        {
            if (AvailbleBalance.Value < transaction.Amount.Value)
                throw new InvalidOperationException();

            Transactions.Add(transaction);
            AvailbleBalance = new Amount(AvailbleBalance.Value - transaction.Amount.Value, AvailbleBalance.Currency);
        }

        public void Register(BankAccountModel bankAccountModel)
        {
            Id = Guid.NewGuid();
            OwnerName = bankAccountModel.OwnerName;

            var @event = new BankAccountRegistered
            {
                OwnerName = OwnerName,
                AvailableBalance = AvailbleBalance
            };

            AddEvent(@event);
        }

        public void InititateTransaction(Transaction transaction)
        {
            AddTransaction(transaction);

            var @event = new TransactionInitiated
            {
                ObjectId = Id,
                Amount = AvailbleBalance,
            };

            AddEvent(@event);
        }


        private void When(BankAccountRegistered @event)
        {
            Id = @event.ObjectId;
            OwnerName = @event.OwnerName;
            AvailbleBalance = @event.AvailableBalance;
        }

        private void When(TransactionInitiated @event)
        {
            AvailbleBalance = @event.Amount;
            Transactions.Add(new Transaction(@event.Amount));
        }
    }
}
