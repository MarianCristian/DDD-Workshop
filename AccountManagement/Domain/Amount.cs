using Common;

namespace AccountManagement.Domain
{
    public class Amount : IValueObject
    {
        public decimal Value { get; set; }
        public string Currency { get; set; }
        public Amount(decimal value, string currency)
        {
            Value = value;
            Currency = currency;
        }
    }
}
