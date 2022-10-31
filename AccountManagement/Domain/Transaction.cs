using Common;

namespace AccountManagement.Domain
{
    public class Transaction : IEntity
    {
        public string Name { get; private set; }
        public DateTime Timestamp { get; private set; }
        public Amount Amount { get; private set; }

        public Transaction(Amount amount)
        {
            this.Amount = amount;
        }
    }
}
