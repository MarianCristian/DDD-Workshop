using System;
namespace Common.Messages
{
    public interface IQueuedDomainEvent : IDomainEvent
    {
        string QueueName { get; }
    }
}