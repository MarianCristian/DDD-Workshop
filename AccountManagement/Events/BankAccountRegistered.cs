using AccountManagement.Domain;
using Common.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountManagement.Events
{
    public class BankAccountRegistered : IDomainEvent
    {
        public Guid ObjectId { get; set; }
        public string ObjectVersion { get; set; }
        public string System { get; set; }
        public Guid MessageId { get; set; }
        public IMessageVersion MessageVersion { get; set; }
        public DateTime TimeStamp { get; set; }
        public string UserId { get; set; }
        public string OwnerName { get; set; }
        public Amount AvailableBalance { get; set; }
    }
}
