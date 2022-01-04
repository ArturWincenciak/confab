using System;
using System.Collections.Generic;
using Confab.Shared.Abstractions.Queries;

namespace Confab.Modules.Agendas.Application.Agendas.Queries
{
    public sealed record GetAgendaItem(Guid ConferenceId, Guid Id) : IQuery<GetAgendaItem.Result>
    {
        public sealed record Result(Guid Id, Guid ConferenceId, string Title, string Description,
            int Level, IEnumerable<string> Tags, IEnumerable<Result.SpeakerDto> Speakers) : IQueryResult
        {
            public sealed record SpeakerDto(Guid Id, string FullName);
        }
    }
}