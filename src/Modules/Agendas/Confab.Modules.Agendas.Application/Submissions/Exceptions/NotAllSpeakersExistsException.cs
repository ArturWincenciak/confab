using System;
using System.Collections.Generic;
using Confab.Shared.Kernel.Exceptions;

namespace Confab.Modules.Agendas.Application.Submissions.Exceptions;

internal class NotAllSpeakersExistsException : ConfabException
{
    public IEnumerable<Guid> CommandSpeakerIds { get; }
    public IEnumerable<Guid> ExistsSpeakerIds { get; }

    public NotAllSpeakersExistsException(IEnumerable<Guid> commandSpeakerIds, IEnumerable<Guid> existsSpeakerIds)
        : base("Not all speakers exists. " +
               $"Desired speakers: '{string.Join(separator: ',', commandSpeakerIds)}'. " +
               $"Exists speaker: '{string.Join(separator: ',', existsSpeakerIds)}'.")
    {
        CommandSpeakerIds = commandSpeakerIds;
        ExistsSpeakerIds = existsSpeakerIds;
    }
}