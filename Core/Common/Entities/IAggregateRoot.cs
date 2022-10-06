using System;
using Common.Messages;

namespace Common.Entities
{
    public interface IAggregateRoot
    {
        Guid Id { get; set; }
        List<IDomainEvent> AllEvents { get;}
        List<IDomainEvent> PendingEvents { get; }

        void ReconstituteFromHistory(IEnumerable<IDomainEvent> events);
        void AddEvent(IDomainEvent @event);
    }
}

