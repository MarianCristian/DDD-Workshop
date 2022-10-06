using System;
namespace Common.Messages
{
    public interface IServiceBusDomainEvent : IDomainEvent
    {
        string TopicName { get; }
    }
}

