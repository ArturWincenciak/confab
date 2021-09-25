using System;
using System.Collections.Generic;
using System.Linq;

namespace Confab.Shared.Abstractions.Kernel.Types.Base
{
    public abstract class AggregatesRoot<T> : IAggregateRoot
    {
        private readonly List<IDomainEvent> _events = new();
        private bool _incrementedVersion;

        public T Id { get; protected set; }
        public int Version { get; protected set; }
        public IEnumerable<IDomainEvent> Events => _events;

        public void AddEvent(IDomainEvent @event)
        {
            if (!_events.Any())
                IncrementVersion();

            _events.Add(@event);
        }

        public void ClearEvents()
        {
            _events.Clear();
            ResetVersion();
        }

        protected void IncrementVersion()
        {
            if (_incrementedVersion)
                return;

            Version++;
            _incrementedVersion = true;
        }

        private void ResetVersion()
        {
            Version = 0;
            _incrementedVersion = false;
        }

        protected static TAggregateRoot Create<TAggregateRoot>(Func<TAggregateRoot> create)
            where TAggregateRoot : IAggregateRoot
        {
            var entity = create();
            entity.ClearEvents();
            return entity;
        }
    }

    public abstract class AggregateRoot : AggregatesRoot<AggregateId>
    {
    }
}