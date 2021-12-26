using System;
using Confab.Shared.Abstractions.Exceptions;
using Confab.Shared.Kernel.Exceptions;

namespace Confab.Modules.Agendas.Application.CallForProps.Exceptions
{
    internal sealed class CallForPapersNotFoundExistsException : ConfabException
    {
        public CallForPapersNotFoundExistsException(Guid conferenceId)
            : base($"Conference with ID: '{conferenceId}' has no CFP.")
        {
            ConferenceId = conferenceId;
        }

        public Guid ConferenceId { get; }
    }
}