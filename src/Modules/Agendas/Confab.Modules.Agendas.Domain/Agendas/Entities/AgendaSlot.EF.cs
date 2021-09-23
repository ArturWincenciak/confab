using System;
using Confab.Shared.Abstractions.Kernel.Types;

namespace Confab.Modules.Agendas.Domain.Agendas.Entities
{
    public abstract partial class AgendaSlot
    {
        public AgendaTrack Track { get; private set; }

        public AgendaSlot(EntityId id, DateTime from, DateTime to)
        {
            Id = id;
            From = from;
            To = to;
        }
    }
}
