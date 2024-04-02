using Confab.Shared.Kernel.Exceptions;
using Confab.Shared.Kernel.Types.Base;

namespace Confab.Modules.Agendas.Domain.Agendas.Exceptions;

internal class EmptyAgendaTrackNameException : ConfabException
{
    public EmptyAgendaTrackNameException(AggregateId id)
        : base($"Agenda track with ID: '{id}' defines empty name.")
    {
    }
}