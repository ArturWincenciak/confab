using System;
using Confab.Shared.Abstractions.Exceptions;

namespace Confab.Modules.Agendas.Application.CallForProps.Exceptions
{
    internal sealed class CallForPapersClosedExistsException : ConfabException
    {
        public Guid ConferenceId { get; }

        public CallForPapersClosedExistsException(Guid conferenceId)
            : base($"Conference with ID: '{conferenceId}' has closed CFP.")
        {
            ConferenceId = conferenceId;
        }
    }
}