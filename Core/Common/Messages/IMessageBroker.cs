using System;

namespace Common.Messages
{
    public interface IMessageBroker
    {
        void PublishAsync<TEvent>(TEvent @event) where TEvent : IDomainEvent;
    }
}

