using System;
using System.Text.Json;
using Common.Messages;
using Infrastructure.Data.QueryRepository;

namespace Infrastructure.Services
{
    public class DomainEventConversionService : IDomainEventConversionService
    {
        public EventStoreTE GetStorageEvent(Guid id, IDomainEvent @event)
        {
            var payload = JsonSerializer.Serialize(@event);

            return new EventStoreTE
            {
                PartitionKey = id.ToString(),
                RowKey = @event.ObjectVersion,
                ETag = Azure.ETag.All,
                Timestamp = DateTime.UtcNow,
                TypeName = @event.GetType().AssemblyQualifiedName,
                Payload = payload
            };
        }

        public IDomainEvent GetDomainEvent(EventStoreTE eventData)
        {
            var eventType = Type.GetType(eventData.TypeName);

            return (IDomainEvent)JsonSerializer.Deserialize(eventData.Payload, eventType);
        }
    }
}

