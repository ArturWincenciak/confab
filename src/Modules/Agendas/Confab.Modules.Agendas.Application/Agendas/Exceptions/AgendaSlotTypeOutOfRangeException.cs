using Confab.Shared.Kernel.Exceptions;

namespace Confab.Modules.Agendas.Application.Agendas.Exceptions;

internal class AgendaSlotTypeOutOfRangeException : ConfabException
{
    public AgendaSlotTypeOutOfRangeException(string type)
        : base($"Agenda slot type '{type}' is not valid.")
    {
    }
}