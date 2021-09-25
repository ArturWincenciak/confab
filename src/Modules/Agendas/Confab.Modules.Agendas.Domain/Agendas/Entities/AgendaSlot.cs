using System;
using Confab.Modules.Agendas.Domain.CallForPaper.Exceptions;
using Confab.Shared.Abstractions.Kernel.Types.Base;

namespace Confab.Modules.Agendas.Domain.Agendas.Entities
{
    public abstract class AgendaSlot
    {
        public EntityId Id { get; protected set; }
        public DateTime From { get; private set; }
        public DateTime To { get; private set; }
        public AgendaTrack Track { get; }

        protected void ChangeDateRange(DateTime from, DateTime to)
        {
            if (from.Date > to.Date)
                throw new InvalidCallForPapersDateException(from, to);

            From = from;
            To = to;
        }
    }
}