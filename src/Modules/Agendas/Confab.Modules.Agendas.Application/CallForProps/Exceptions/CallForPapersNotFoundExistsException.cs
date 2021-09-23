using System;
using Confab.Shared.Abstractions.Exceptions;

namespace Confab.Modules.Agendas.Application.CallForProps.Exceptions
{
    internal sealed class CallForPapersNotFoundExistsException : ConfabException
    {
        public Guid ConferenceId { get; }

        public CallForPapersNotFoundExistsException(Guid conferenceId)
            : base($"Conference with ID: '{conferenceId}' has no CFP.")
        {
            ConferenceId = conferenceId;
        }
    }
}