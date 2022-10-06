using System;
namespace Common.Messages
{
    public interface IEventPublisher
    {
        void Publish<TEvent>(TEvent @event) where TEvent : IDomainEvent;
    }
}

