using Confab.Shared.Kernel.Exceptions;
using Confab.Shared.Kernel.Types.Base;

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