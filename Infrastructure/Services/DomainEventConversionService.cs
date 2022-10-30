using System;
using Common.Messages;
using Infrastructure.Data.QueryRepository;
using Newtonsoft.Json;

namespace Infrastructure.Services
{
    public class DomainEventConversionService : IDomainEventConversionService
    {
        public EventStoreTE GetStorageEvent(Guid id, IDomainEvent @event)
        {
            var payload = JsonConvert.SerializeObject(@event);

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

            return (IDomainEvent)JsonConvert.DeserializeObject(eventData.Payload, eventType);
        }
    }
}

