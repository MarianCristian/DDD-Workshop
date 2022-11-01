using AccountManagement.Domain;
using AccountManagement.Projections;
using AccountManagement.Queries;
using Common.Messages;
using Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountManagement.QueryHandlers
{
    public class BankAccountQueryHandler : IHandleQuery<BankAccountQuery, BankAccountProjection>
    {
        private readonly IEventStore eventStore;
        public BankAccountQueryHandler(IEventStore eventStore)
        {
            this.eventStore = eventStore;
        }
        public BankAccountProjection Handle(BankAccountQuery query)
        {
            var bankAccount = eventStore.GetById<BankAccount>(query.BankAccountId);

            return new BankAccountProjection
            {
                OwnerName = bankAccount.OwnerName
            };
        }
    }
}
