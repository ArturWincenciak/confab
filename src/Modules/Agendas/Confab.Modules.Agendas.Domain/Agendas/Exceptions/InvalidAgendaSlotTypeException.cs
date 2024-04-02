using Confab.Shared.Kernel.Exceptions;
using Confab.Shared.Kernel.Types.Base;

namespace Confab.Modules.Agendas.Domain.Agendas.Exceptions;

internal class InvalidAgendaSlotTypeException : ConfabException
{
    public InvalidAgendaSlotTypeException(EntityId agendaSlotId)
        : base($"Agenda slot with ID: '{agendaSlotId}' has type " +
               "which does not allow to perform descried operation.")
    {
    }
}