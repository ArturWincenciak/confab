using System;
using Confab.Shared.Abstractions.Exceptions;

namespace Confab.Modules.Agendas.Application.Agendas.Exceptions
{
    internal sealed class AgendaTrackAlreadyExistsException : ConfabException
    {
        public AgendaTrackAlreadyExistsException(Guid agendaTrackId)
            : base($"Agenda track with ID: '{agendaTrackId}' already exists.")
        {
        }
    }
}