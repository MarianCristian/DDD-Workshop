using System;
using Common.Entities;
using Common.Services;
using Infrastructure.Data.TableStorage;
using Infrastructure.Services;

namespace Infrastructure.Data.QueryRepository
{
    public class EventStore : IEventStore
    {
        private readonly ITSContext tsContext;
        private readonly IDomainEventConversionService domainConversionService;

        public EventStore(ITSContext tsContext, IDomainEventConversionService domainConversionService)
        {
            this.tsContext = tsContext;
            this.domainConversionService = domainConversionService;
        }

        public TAggregateRoot GetById<TAggregateRoot>(Guid aggregateRootId) where TAggregateRoot : IAggregateRoot, new()
        {
            var events = tsContext.GetTableDataById<EventStoreTE>(aggregateRootId, GetTableName(typeof(TAggregateRoot)));

            var aggregate = new TAggregateRoot();

            aggregate.ReconstituteFromHistory(events.Select(e => domainConversionService.GetDomainEvent(e)));

            return aggregate;
        }

        public void Save<TAggregate>(TAggregate aggregate) where TAggregate : IAggregateRoot
        {
            foreach (var @event in aggregate.PendingEvents)
            {
                var eventData = domainConversionService.GetStorageEvent(aggregate.Id, @event);
                tsContext.Insert(eventData, GetTableName(aggregate.GetType()));
            }
        }

        private string GetTableName(Type type)
        {
            return $"EventStore{type.FullName.Split(',')[0].Split('.')[1]}";
        }
    }
}

