using System;
using Confab.Modules.Agendas.Domain.Agendas.Exceptions;
using Confab.Shared.Abstractions.Kernel.Types;

namespace Confab.Modules.Agendas.Domain.Agendas.Entities
{
    public sealed partial class RegularAgendaSlot : AgendaSlot
    {
        public AgendaItem AgendaItem { get; private set; }
    }

    public sealed partial class RegularAgendaSlot : AgendaSlot
    {
        public RegularAgendaSlot(EntityId id)
            : base(id)
        {
        }

        public int? ParticipantLimit { get; private set; }

        internal static RegularAgendaSlot Create(EntityId id, DateTime from, DateTime to, int? participantsLimit)
        {
            var entity = new RegularAgendaSlot(id);
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