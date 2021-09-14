using System;
using System.Collections.Generic;
using Confab.Shared.Abstractions.Queries;

namespace Confab.Modules.Agendas.Application.Submissions.Queries
{
    public sealed record GetSubmission(Guid Id) : IQuery<GetSubmission.SubmissionDto>
    {
        internal sealed record SubmissionDto(Guid Id, Guid ConferenceId, string Title, string Description, int Level,
            string Status, IEnumerable<string> Tags, IEnumerable<SpeakerDto> Speakers) : IQueryResult;

        internal sealed record SpeakerDto(Guid Id, string FullName);
    }
}