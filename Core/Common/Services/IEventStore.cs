using System;
using Common.Entities;

namespace Common.Services
{
    public interface IEventStore
    {
        void Save<TAggregate>(TAggregate aggregate) where TAggregate : IAggregateRoot;
        TAggregateRoot GetById<TAggregateRoot>(Guid aggregateRootId) where TAggregateRoot : IAggregateRoot, new();
    }
}

