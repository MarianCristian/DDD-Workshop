using AccountManagement.Events;
using Common.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountManagement.EventHandlers
{
    public class NotificationEventHandler : 
        IEventHandler<BankAccountRegistered>,
        IEventHandler<TransactionInitiated>
    {
        public void Handle(BankAccountRegistered @event)
        {
            // Send email to customer
        }

        public void Handle(TransactionInitiated @event)
        {
            // Send email to customer
        }
    }
}
