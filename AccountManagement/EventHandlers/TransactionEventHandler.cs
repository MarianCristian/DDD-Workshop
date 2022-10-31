using AccountManagement.Events;
using Common.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountManagement.EventHandlers
{
    public class TransactionEventHandler : IEventHandler<TransactionInitiated>
    {
        public void Handle(TransactionInitiated @event)
        {
            //Initialize sql transaction
        }
    }
}
