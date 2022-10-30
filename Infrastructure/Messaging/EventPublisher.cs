using System.Reflection;
using Common.Messages;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Messaging
{
    public class EventPublisher : IEventPublisher
    {
        private readonly IMessageBroker messageBroker;
        private readonly IServiceProvider serviceProvider;

        public EventPublisher(IMessageBroker messageBroker, IServiceProvider serviceProvider)
        {
            this.messageBroker = messageBroker;
            this.serviceProvider = serviceProvider;
        }

        public void Publish<TEvent>(TEvent @event) where TEvent : IDomainEvent
        {
            if (@event is IServiceBusDomainEvent)
                this.messageBroker.PublishAsync(@event);
            else
                Handle(@event);
        }

        private void Handle<TEvent>(TEvent @event) where TEvent : IDomainEvent
        {
            var method = typeof(EventPublisher).GetMethod(nameof(HandleEvent), BindingFlags.NonPublic | BindingFlags.Instance);
            method.MakeGenericMethod(@event.GetType()).Invoke(this, new object[] { @event });
        }

        private void HandleEvent<TEvent>(TEvent message) where TEvent : IDomainEvent
        {
            var eventHandlers = serviceProvider.GetServices<IEventHandler<TEvent>>();
            Parallel.ForEach(eventHandlers, eventHandler =>
            {
                eventHandler.Handle(message);
            });
        }
    }
}

