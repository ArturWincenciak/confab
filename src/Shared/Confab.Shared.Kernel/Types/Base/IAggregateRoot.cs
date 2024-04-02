﻿using System.Collections.Generic;

namespace Confab.Shared.Kernel.Types.Base;

public interface IAggregateRoot
{
    int Version { get; }
    IEnumerable<IDomainEvent> Events { get; }
    void AddEvent(IDomainEvent @event);
    void ClearEvents();
}