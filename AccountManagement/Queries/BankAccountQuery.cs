using AccountManagement.Projections;
using Common.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountManagement.Queries
{
    public class BankAccountQuery : IQuery<BankAccountProjection>
    {
        public Guid BankAccountId { get; set; }
    }
}
