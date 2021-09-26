using System;
using System.Collections.Generic;
using Confab.Shared.Abstractions.Queries;

namespace Confab.Modules.Agendas.Application.Agendas.Queries
{
    public sealed record GetAgendaTrack(Guid Id) : IQuery<GetAgendaTrack.AgendaTrackDto>
    {
        //todo: why slots are objects
        public sealed record AgendaTrackDto(Guid Id, Guid ConferenceId, string Name,
            IEnumerable<object> Slots) : IQueryResult;
    }
}