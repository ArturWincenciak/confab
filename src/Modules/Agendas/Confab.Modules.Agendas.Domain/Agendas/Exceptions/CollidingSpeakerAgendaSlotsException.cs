using Confab.Shared.Abstractions.Kernel.Types.Base;
using Confab.Shared.Kernel.Exceptions;

namespace Confab.Modules.Agendas.Domain.Agendas.Exceptions
{
    internal class CollidingSpeakerAgendaSlotsException : ConfabException
    {
        public CollidingSpeakerAgendaSlotsException(EntityId agendaSlotId, AggregateId agendaItemId)
            : base($"Cannot assign agenda item with ID: '{agendaItemId}' to slot with ID: '{agendaSlotId}'.")
        {
        }
    }
}