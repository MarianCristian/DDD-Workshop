using System;
using Common.Messages;
using Infrastructure.Data.QueryRepository;

namespace Infrastructure.Services
{
    public interface IDomainEventConversionService
    {
        EventStoreTE GetStorageEvent(Guid id, IDomainEvent @event);
        IDomainEvent GetDomainEvent(EventStoreTE eventData);
    }
}

