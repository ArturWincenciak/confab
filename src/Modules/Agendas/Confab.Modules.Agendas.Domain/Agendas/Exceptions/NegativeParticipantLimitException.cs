using System;
using Confab.Shared.Abstractions.Exceptions;

namespace Confab.Modules.Agendas.Domain.Agendas.Exceptions
{
    internal class NegativeParticipantLimitException : ConfabException
    {
        public NegativeParticipantLimitException(Guid agendaSlotId)
            : base($"Regular slot with ID: '{agendaSlotId}' defines negative participants limit.")
        {
        }
    }
}