using System;
using System.Collections.Generic;
using Confab.Shared.Abstractions.Queries;

namespace Confab.Modules.Agendas.Application.Submissions.Queries;

public sealed record GetSubmission(Guid Id) : IRequestMessage<GetSubmission.Result>
{
    public sealed record Result(Guid Id,
        Guid ConferenceId,
        string Title,
        string Description,
        int Level,
        string Status,
        IEnumerable<string> Tags,
        IEnumerable<Result.SpeakerDto> Speakers) : IResponseMessage
    {
        public sealed record SpeakerDto(Guid Id, string FullName);
    }
}