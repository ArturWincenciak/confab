using System;
using Confab.Shared.Kernel.Exceptions;

namespace Confab.Modules.Attendances.Domain.Exceptions
{
    public class ParticipantNotFoundException : ConfabException
    {
        public ParticipantNotFoundException(Guid conferenceId, Guid participantId)
            : base($"Participant of conference: '{conferenceId}' with participant ID: '{participantId}' was not found.")
        {
            ConferenceId = conferenceId;
            ParticipantId = participantId;
        }

        public Guid ConferenceId { get; }
        public Guid ParticipantId { get; }
    }
}