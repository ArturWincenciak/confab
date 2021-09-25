using System;
using Confab.Modules.Agendas.Domain.Agendas.Exceptions;
using Confab.Shared.Abstractions.Kernel.Types;
using Confab.Shared.Abstractions.Kernel.Types.Base;

namespace Confab.Modules.Agendas.Domain.Agendas.Entities
{
    public sealed class RegularAgendaSlot : AgendaSlot
    {
        public int? ParticipantLimit { get; private set; }
        public AgendaItem AgendaItem { get; }

        internal static RegularAgendaSlot Create(EntityId id, DateTime from, DateTime to, int? participantsLimit)
        {
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
    }
}