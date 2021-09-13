using System;
using System.Collections.Generic;
using Confab.Shared.Abstractions.Exceptions;

namespace Confab.Modules.Agendas.Application.Submissions.Exceptions
{
    internal class NotAllSpeakersExistsException : ConfabException
    {
        public NotAllSpeakersExistsException(IEnumerable<Guid> commandSpeakerIds, IEnumerable<Guid> existsSpeakerIds)
            : base("Not all speakers exists. " +
                   $"Desired speakers: '{string.Join(',', commandSpeakerIds)}'. " +
                   $"Exists speaker: '{string.Join(',', existsSpeakerIds)}'.")
        {
            CommandSpeakerIds = commandSpeakerIds;
            ExistsSpeakerIds = existsSpeakerIds;
        }

        public IEnumerable<Guid> CommandSpeakerIds { get; }
        public IEnumerable<Guid> ExistsSpeakerIds { get; }
    }
}