using System.Threading.Tasks;
using Confab.Modules.Agendas.Application.Agendas.Queries;
using Confab.Modules.Agendas.Domain.Agendas.Entities;
using Confab.Modules.Agendas.Infrastructure.EF.Mappings;
using Confab.Shared.Abstractions.Queries;
using Microsoft.EntityFrameworkCore;

namespace Confab.Modules.Agendas.Infrastructure.EF.Queries.Handlers;

internal sealed class GetAgendaTrackHandler : IQueryHandler<GetAgendaTrack, GetAgendaTrack.Result>
{
    private readonly DbSet<AgendaTrack> _agendaTracks;

    public GetAgendaTrackHandler(AgendasDbContext context) =>
        _agendaTracks = context.AgendaTracks;

    public async Task<GetAgendaTrack.Result> HandleAsync(GetAgendaTrack query)
    {
        var agendaTrack = await _agendaTracks
            .AsNoTracking()
            .Include(x => x.Slots)
            .ThenInclude(x => (x as RegularAgendaSlot).AgendaItem)
            .ThenInclude(x => x.Speakers)
            .SingleOrDefaultAsync(x => x.Id == query.Id);

        return agendaTrack is not null ? AsDto(agendaTrack) : null;
    }

    private static GetAgendaTrack.Result AsDto(AgendaTrack agendaTrack) =>
        new(
            agendaTrack.Id,
            agendaTrack.ConferenceId,
            agendaTrack.Name,
            Slots: MappingsExtension.AsDto(agendaTrack.Slots)
        );
}