using System;
using Common.Entities;

namespace Common.Factories
{
    public interface IAggregateRootFactory<TAggregate> where TAggregate : IAggregateRoot
    {
        TAggregate Create();
        TAggregate Create(Guid id);
    }
}

