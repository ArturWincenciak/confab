using System;
using System.Collections;
using System.Collections.Generic;
using Confab.Shared.Abstractions.Queries;

namespace Confab.Modules.Agendas.Application.Submissions.Queries;

public sealed record BrowseSubmissions(Guid? ConferenceId, Guid? SpeakerId) : IRequestMessage<BrowseSubmissions.Result>
{
    public sealed class Result : IEnumerable<Result.SubmissionDto>,
        IResponseMessage
    {
        private readonly IEnumerable<SubmissionDto> _submissions;

        public Result(IEnumerable<SubmissionDto> submissions) =>
            _submissions = submissions;

        IEnumerator IEnumerable.GetEnumerator() =>
            GetEnumerator();

        public IEnumerator<SubmissionDto> GetEnumerator() =>
            _submissions.GetEnumerator();

        public sealed record SubmissionDto(Guid Id,
            Guid ConferenceId,
            string Title,
            string Description,
            int Level,
            string Status,
            IEnumerable<string> Tags,
            IEnumerable<SubmissionDto.SpeakerDto> Speakers)
        {
            public sealed record SpeakerDto(Guid Id, string FullName);
        }
    }
}