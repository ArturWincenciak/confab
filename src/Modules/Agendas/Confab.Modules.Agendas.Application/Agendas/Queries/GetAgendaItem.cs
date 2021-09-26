using System;
using System.Collections.Generic;
using Confab.Shared.Abstractions.Queries;

namespace Confab.Modules.Agendas.Application.Agendas.Queries
{
    public sealed record GetAgendaItem(Guid Id) : IQuery<GetAgendaItem.AgendaItemDto>
    {
        public sealed record AgendaItemDto(Guid Id, Guid ConferenceId, string Title, string Description,
            int Level, IEnumerable<string> Tags, IEnumerable<SpeakerDto> Speakers) : IQueryResult;

        public sealed record SpeakerDto(Guid Id, string FullName);
    }
}