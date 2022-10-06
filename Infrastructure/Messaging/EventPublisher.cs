using System;
using Common.Messages;

namespace Infrastructure.Messaging
{
    public class EventPublisher : IEventPublisher
    {
        private readonly IMessageBroker messageBroker;

        public EventPublisher(IMessageBroker messageBroker)
        {
            this.messageBroker = messageBroker;
        }

        public void Publish<TEvent>(TEvent @event) where TEvent : IDomainEvent
        {
            this.messageBroker.PublishAsync(@event);
        }
    }
}

