using System;
using System.Linq;
using System.Threading.Tasks;
using Confab.Modules.Agendas.Application.Agendas.Queries;
using Confab.Modules.Agendas.Domain.Agendas.Entities;
using Confab.Shared.Abstractions.Queries;
using Confab.Shared.Abstractions.Storage;
using Microsoft.EntityFrameworkCore;

namespace Confab.Modules.Agendas.Infrastructure.EF.Queries.Handlers
{
    internal sealed class GetAgendaHandler : IQueryHandler<GetAgenda, GetAgenda.AgendaDto>
    {
        private readonly DbSet<AgendaTrack> _agendaTracks;
        private readonly IRequestStorage _requestStorage;

        public GetAgendaHandler(AgendasDbContext context, IRequestStorage requestStorage)
        {
            _agendaTracks = context.AgendaTracks;
            _requestStorage = requestStorage;
        }

        public async Task<GetAgenda.AgendaDto> HandleAsync(GetAgenda query)
        {
            var storageKey = GetStorageKey(query.ConferenceId);
            var cached = _requestStorage.Get<GetAgenda.AgendaDto>(storageKey);
            if (cached is not null)
                return cached;

            var agendaTracks = await _agendaTracks
                .Include(x => x.Slots)
                .ThenInclude(x => (x as RegularAgendaSlot).AgendaItem)
                .ThenInclude(x => x.Speakers)
                .Where(x => x.ConferenceId == query.ConferenceId)
                .ToListAsync();

            var agendaTracksDto = agendaTracks?.Select(x => AsDto(x));
            var resultDto = new GetAgenda.AgendaDto(agendaTracksDto);

            _requestStorage.Set(storageKey, resultDto, TimeSpan.FromSeconds(5));

            return resultDto;
        }

        private static GetAgenda.AgendaTrackDto AsDto(AgendaTrack agendaTrack)
        {
            return new GetAgenda.AgendaTrackDto
            (
                agendaTrack.Id,
                agendaTrack.ConferenceId,
                agendaTrack.Name,
                agendaTrack.Slots
            );
        }

        private static string GetStorageKey(Guid conferenceId)
        {
            return $"agenda/{conferenceId}";
        }
    }
}