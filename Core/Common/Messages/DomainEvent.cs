using System;
namespace Common.Messages
{
    public class DomainEvent : IDomainEvent
    {
        public Guid ObjectId { get; set; }
        public string ObjectVersion { get; set; }
        public string UserId { get; set; }
        public string System { get; set; }
        public Guid MessageId { get; set; }
        public IMessageVersion MessageVersion { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}

