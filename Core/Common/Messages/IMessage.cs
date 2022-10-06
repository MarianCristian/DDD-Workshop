using System;
namespace Common.Messages
{
    public interface IMessage
    {
        public Guid MessageId { get; set; }
        public IMessageVersion MessageVersion { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}

