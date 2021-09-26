using System;
using System.Collections;
using System.Collections.Generic;
using Confab.Shared.Abstractions.Queries;

namespace Confab.Modules.Agendas.Application.Agendas.Queries
{
    public sealed record GetAgenda(Guid ConferenceId) : IQuery<GetAgenda.AgendaDto>
    {
        public class AgendaDto : IEnumerable<AgendaTrackDto>, IQueryResult
        {
            private readonly IEnumerable<AgendaTrackDto> _agendaTracks;

            public AgendaDto(IEnumerable<AgendaTrackDto> agendaTracks)
            {
                _agendaTracks = agendaTracks;
            }

            public IEnumerator<AgendaTrackDto> GetEnumerator()
            {
                return _agendaTracks.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        //todo: dlaczego tutaj jest sloty są objectami?
        public sealed record AgendaTrackDto(Guid Id, Guid ConferenceId, string Name, IEnumerable<object> Slots);
    }
}