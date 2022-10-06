using System;
using Azure.Messaging.ServiceBus;
using Common.Messages;
using Newtonsoft.Json;

namespace Infrastructure.Messaging
{
    public class ServiceBusEventPublisher : IMessageBroker
    {
        private readonly ServiceBusClient serviceBusClient;

        public ServiceBusEventPublisher(ServiceBusClient serviceBusClient)
        {
            this.serviceBusClient = serviceBusClient;
        }

        public async void PublishAsync<TEvent>(TEvent @event) where TEvent : IDomainEvent
        {
            var queueOrTopicName = GetQueueOrTopicName(@event);
            var sender = serviceBusClient.CreateSender(queueOrTopicName);

            try
            {
                var message = new ServiceBusMessage(JsonConvert.SerializeObject(@event))
                {
                    SessionId = queueOrTopicName,
                    MessageId = @event.MessageId.ToString(),
                    ContentType = @event.GetType().AssemblyQualifiedName,
                };
                await sender.SendMessageAsync(message);
            }
            finally
            {
                await sender.DisposeAsync();
            }
        }

        private string GetQueueOrTopicName(IDomainEvent @event)
        {
            if (@event is IQueuedDomainEvent)
                return (@event as IQueuedDomainEvent).QueueName;

            return (@event as IServiceBusDomainEvent).TopicName;
        }
    }
}

