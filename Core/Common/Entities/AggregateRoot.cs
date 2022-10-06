using System;
using Common.Messages;

namespace Common.Entities
{
    public class AggregateRoot : IAggregateRoot
    {
        public Guid Id { get; set; }
        public List<IDomainEvent> PendingEvents { get; } = new List<IDomainEvent>();
        public List<IDomainEvent> AllEvents { get; } = new List<IDomainEvent>();

        protected Dictionary<Type, Action<IDomainEvent>> Handlers = new Dictionary<Type, Action<IDomainEvent>>();

        public void AddHandler<TEvent>(Action<TEvent> handler) where TEvent : IDomainEvent
        {
            this.Handlers.Add(typeof(TEvent), (@event) => handler((TEvent)@event));
        }

        public void ReconstituteFromHistory(IEnumerable<IDomainEvent> events)
        {
            this.AllEvents.AddRange(events);

            foreach (IDomainEvent @event in events)
                Handlers[@event.GetType()](@event);
        }

        public void AddEvent(IDomainEvent @event)
        {
            @event.ObjectId = Id;
            @event.MessageId = Guid.NewGuid();
            @event.ObjectVersion = GetNextVersion(@event.ObjectId);
            @event.TimeStamp = DateTime.UtcNow;        

            AllEvents.Add(@event);
            PendingEvents.Add(@event);
        }

        public string GetVersion(Guid objectId = default)
        {
            if (objectId == Guid.Empty)
                objectId = Id;

            return AllEvents.Where(e => e.ObjectId == objectId).Max(x => x.ObjectVersion);
        }

        private string GetNextVersion(Guid objectId)
        {
            var versionNumber = 0;

            if (AllEvents.Count > 0 && AllEvents.Any(e => e.ObjectId == objectId))
            {
                var lastVersion = AllEvents.Where(e => e.ObjectId == objectId).Max(e => e.ObjectVersion);

                versionNumber = int.Parse(lastVersion.Split('|').Last());
            }

            return $"{objectId}|{versionNumber + 1:D10}";
        }
    }
}

