using System.Linq;
using System.Threading.Tasks;
using Confab.Modules.Agendas.Application.Agendas.Queries;
using Confab.Modules.Agendas.Domain.Agendas.Entities;
using Confab.Modules.Agendas.Domain.Submissions.Entities;
using Confab.Shared.Abstractions.Queries;
using Microsoft.EntityFrameworkCore;

namespace Confab.Modules.Agendas.Infrastructure.EF.Queries.Handlers
{
    internal sealed class BrowseAgendaItemsHandler : IQueryHandler<BrowsAgendaItems, BrowsAgendaItems.AgendaItemsDto>
    {
        private readonly DbSet<AgendaItem> _agendaItems;

        public BrowseAgendaItemsHandler(AgendasDbContext context)
        {
            _agendaItems = context.AgendaItems;
        }

        public async Task<BrowsAgendaItems.AgendaItemsDto> HandleAsync(BrowsAgendaItems query)
        {
            var items = await _agendaItems
                .AsNoTracking()
                .Include(x => x.Speakers)
                .Where(x => x.ConferenceId == query.ConferenceId)
                .Select(x => AsDto(x))
                .ToListAsync();

            return new BrowsAgendaItems.AgendaItemsDto(items);
        }

        private BrowsAgendaItems.AgendaItemsDto.AgendaItemDto AsDto(AgendaItem agendaItem)
        {
            return new BrowsAgendaItems.AgendaItemsDto.AgendaItemDto(
                agendaItem.Id,
                agendaItem.ConferenceId,
                agendaItem.Title,
                agendaItem.Description,
                agendaItem.Level,
                agendaItem.Tags,
                agendaItem.Speakers.Select(AsDto));
        }

        private static BrowsAgendaItems.AgendaItemsDto.AgendaItemDto.SpeakerDto AsDto(Speaker agendaItemSpeaker)
        {
            return new BrowsAgendaItems.AgendaItemsDto.AgendaItemDto.SpeakerDto(
                agendaItemSpeaker.Id,
                agendaItemSpeaker.FullName);
        }
    }
}