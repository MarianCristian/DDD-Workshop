using System;
namespace Common.Messages
{
    public interface IEventHandler<TEvent> where TEvent : IDomainEvent
    {
        void Handle(TEvent @event);
    }

    public interface IQueuedEventHandler<TEvent> where TEvent : IQueuedDomainEvent
    {
        void Handle(TEvent @event);
    }
}

