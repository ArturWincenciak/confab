using System.Linq;
using System.Threading.Tasks;
using Confab.Modules.Agendas.Application.Agendas.Queries;
using Confab.Modules.Agendas.Domain.Agendas.Entities;
using Confab.Modules.Agendas.Domain.Submissions.Entities;
using Confab.Shared.Abstractions.Queries;
using Microsoft.EntityFrameworkCore;

namespace Confab.Modules.Agendas.Infrastructure.EF.Queries.Handlers;

internal sealed class BrowseAgendaItemsHandler : IQueryHandler<BrowsAgendaItems, BrowsAgendaItems.Result>
{
    private readonly DbSet<AgendaItem> _agendaItems;

    public BrowseAgendaItemsHandler(AgendasDbContext context) =>
        _agendaItems = context.AgendaItems;

    public async Task<BrowsAgendaItems.Result> HandleAsync(BrowsAgendaItems query)
    {
        var items = await _agendaItems
            .AsNoTracking()
            .Include(x => x.Speakers)
            .Where(x => x.ConferenceId == query.ConferenceId)
            .Select(x => AsDto(x))
            .ToListAsync();

        return new(items);
    }

    private static BrowsAgendaItems.Result.AgendaItemDto AsDto(AgendaItem agendaItem) =>
        new(
            agendaItem.Id,
            agendaItem.ConferenceId,
            agendaItem.Title,
            agendaItem.Description,
            agendaItem.Level,
            agendaItem.Tags,
            Speakers: agendaItem.Speakers.Select(AsDto));

    private static BrowsAgendaItems.Result.AgendaItemDto.SpeakerDto AsDto(Speaker agendaItemSpeaker) =>
        new(
            agendaItemSpeaker.Id,
            agendaItemSpeaker.FullName);
}