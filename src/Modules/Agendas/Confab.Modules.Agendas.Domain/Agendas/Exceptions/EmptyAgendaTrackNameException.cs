using System;
using Confab.Shared.Abstractions.Exceptions;
using Confab.Shared.Abstractions.Kernel.Types.Base;

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