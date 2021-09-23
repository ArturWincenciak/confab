using System;
using Confab.Modules.Agendas.Domain.CallForPaper.Exceptions;
using Confab.Shared.Abstractions.Kernel.Types;

namespace Confab.Modules.Agendas.Domain.Agendas.Entities
{
    public abstract partial class AgendaSlot
    {
        protected AgendaSlot(EntityId id)
        {
            Id = id;
        }

        public EntityId Id { get; }
        public DateTime From { get; private set; }
        public DateTime To { get; private set; }

        protected void ChangeDateRange(DateTime from, DateTime to)
        {
            if (from.Date > to.Date)
                throw new InvalidCallForPapersDateException(from, to);

            From = from;
            To = to;
        }
    }
}