using System;
using Confab.Shared.Kernel.Exceptions;

namespace Confab.Modules.Attendances.Domain.Exceptions;

public class ParticipantNotFoundException : ConfabException
{
    public Guid ConferenceId { get; }
    public Guid ParticipantId { get; }

    public ParticipantNotFoundException(Guid conferenceId, Guid participantId)
        : base($"Participant with ID '{participantId}' of conference: '{conferenceId}' was not found.")
    {
        ConferenceId = conferenceId;
        ParticipantId = participantId;
    }
}