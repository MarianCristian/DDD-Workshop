using AccountManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountManagement.DTO
{
    public class TransactionModel
    {
        public Guid BankAccountId { get; set; }
        public Amount Amount { get; set; }
    }
}
