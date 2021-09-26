using System;
using System.Collections.Generic;
using Confab.Shared.Abstractions.Queries;

namespace Confab.Modules.Agendas.Application.Agendas.Queries
{
    public sealed record GetRegularAgendaSlot(Guid AgendaItemId) : IQuery<GetRegularAgendaSlot.RegularAgendaSlotDot>
    {
        public sealed record RegularAgendaSlotDot(int? ParticipantLimit, AgendaItemDto AgendaItem) : IQueryResult;

        public sealed record AgendaItemDto(Guid Id, Guid ConferenceId, string Title, string Description, int Level,
            IEnumerable<string> Tags, IEnumerable<SpeakerDto> Speakers);

        public sealed record SpeakerDto(Guid Id, string FullName);
    }
}