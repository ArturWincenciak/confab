using System;
using System.Collections;
using System.Collections.Generic;
using Confab.Shared.Abstractions.Queries;

namespace Confab.Modules.Attendances.Application.Clients.Agendas.DTO
{
    public sealed record AgendaTracksDto : IEnumerable<AgendaTracksDto.AgendaTrackDto>, IModuleResponse
    {
        private readonly IEnumerable<AgendaTrackDto> _agendaTracks;

        public AgendaTracksDto(IEnumerable<AgendaTrackDto> agendaTracks)
        {
            _agendaTracks = agendaTracks;
        }

        public IEnumerator<AgendaTrackDto> GetEnumerator()
        {
            return _agendaTracks.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _agendaTracks.GetEnumerator();
        }

        public sealed record AgendaTrackDto(
            Guid Id,
            Guid ConferenceId,
            string Name,
            IEnumerable<RegularAgendaSlotDto> Slots);
    }
}