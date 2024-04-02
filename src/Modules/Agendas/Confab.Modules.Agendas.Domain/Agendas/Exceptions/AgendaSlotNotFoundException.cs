using Confab.Shared.Kernel.Exceptions;
using Confab.Shared.Kernel.Types.Base;

namespace Confab.Modules.Agendas.Domain.Agendas.Exceptions;

internal class AgendaSlotNotFoundException : ConfabException
{
    public AgendaSlotNotFoundException(EntityId agendaSlotId)
        : base($"Agenda slot with ID: '{agendaSlotId}' was not found.")
    {
    }
}