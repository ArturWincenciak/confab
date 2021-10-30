using System;
using Confab.Modules.Agendas.Domain.Agendas.Exceptions;

namespace Confab.Modules.Agendas.Domain.Agendas.Entities
{
    public sealed class RegularAgendaSlot : AgendaSlot
    {
        public int? ParticipantLimit { get; private set; }
        public AgendaItem AgendaItem { get; private set; }

        public static RegularAgendaSlot Create(DateTime from, DateTime to, int? participantsLimit)
        {
            var id = Guid.NewGuid();
            var entity = new RegularAgendaSlot {Id = id};
            entity.ChangeDateRange(from, to);
            entity.ChangeParticipantLimit(participantsLimit);
            return entity;
        }

        private void ChangeParticipantLimit(int? participantsLimit)
        {
            if (participantsLimit < 0)
                throw new NegativeParticipantLimitException(Id);

            ParticipantLimit = participantsLimit;
        }

        internal void ChangeAgendaItem(AgendaItem agendaItem)
        {
            AgendaItem = agendaItem;
        }
    }
}