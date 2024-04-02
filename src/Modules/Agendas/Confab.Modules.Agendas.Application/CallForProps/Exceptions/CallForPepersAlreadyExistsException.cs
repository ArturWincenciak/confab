using System;
using Confab.Shared.Kernel.Exceptions;

namespace Confab.Modules.Agendas.Application.CallForProps.Exceptions;

internal sealed class CallForPapersAlreadyExistsException : ConfabException
{
    public Guid ConferenceId { get; }

    public CallForPapersAlreadyExistsException(Guid conferenceId)
        : base($"Conference with ID: '{conferenceId}' already defined CFP.") =>
        ConferenceId = conferenceId;
}