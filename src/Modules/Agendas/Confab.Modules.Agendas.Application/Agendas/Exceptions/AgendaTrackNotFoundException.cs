using System;
using Confab.Shared.Abstractions.Exceptions;
using Confab.Shared.Kernel.Exceptions;

namespace Confab.Modules.Agendas.Application.Agendas.Exceptions
{
    internal sealed class AgendaTrackNotFoundException : ConfabException
    {
        public AgendaTrackNotFoundException(Guid agendaTrackId)
            : base($"Agenda track with ID: '{agendaTrackId}' was not found.")
        {
        }
    }
}