using Confab.Shared.Abstractions.Kernel.Types.Base;
using Confab.Shared.Kernel.Exceptions;

namespace Confab.Modules.Agendas.Domain.Agendas.Exceptions
{
    internal class EmptyAgendaTrackNameException : ConfabException
    {
        public EmptyAgendaTrackNameException(AggregateId id)
            : base($"Agenda track with ID: '{id}' defines empty name.")
        {
        }
    }
}