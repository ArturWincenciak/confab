﻿using System.Threading.Tasks;

namespace Confab.Shared.Abstractions.Kernel
{
    public interface IDomainEventDispatcher
    {
        Task SendAsync(params IDomainEvent[] events);
    }
}