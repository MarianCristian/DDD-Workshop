using AccountManagement.Events;
using Common.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountManagement.EventHandlers
{
    public class BankAccountEventHandler : 
        IEventHandler<BankAccountRegistered>,
        IEventHandler<TransactionInitiated>
    {
        public void Handle(BankAccountRegistered @event)
        {
            // Update BankAcccount Table

        }

        public void Handle(TransactionInitiated @event)
        {
            //Modify something on account
        }
    }
}
