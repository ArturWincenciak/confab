using System.Linq;
using System.Threading.Tasks;
using Confab.Modules.Agendas.Application.Agendas.Queries;
using Confab.Modules.Agendas.Domain.Agendas.Entities;
using Confab.Shared.Abstractions.Queries;
using Microsoft.EntityFrameworkCore;

namespace Confab.Modules.Agendas.Infrastructure.EF.Queries.Handlers
{
    internal sealed class GetAgendaItemHandler : IQueryHandler<GetAgendaItem, GetAgendaItem.Result>
    {
        private readonly DbSet<AgendaItem> _agendaItems;

        public GetAgendaItemHandler(AgendasDbContext context)
        {
            _agendaItems = context.AgendaItems;
        }

        public async Task<GetAgendaItem.Result> HandleAsync(GetAgendaItem query)
        {
            return await _agendaItems
                .AsNoTracking()
                .Include(x => x.Speakers)
                .Where(x => x.Id == query.Id)
                .Select(x => AsDto(x))
                .FirstOrDefaultAsync();
        }

        private static GetAgendaItem.Result AsDto(AgendaItem entity)
        {
            return new GetAgendaItem.Result
            (
                entity.Id,
                entity.ConferenceId,
                entity.Title,
                entity.Description,
                entity.Level,
                entity.Tags,
                entity.Speakers.Select(entitySpeaker => new GetAgendaItem.Result.SpeakerDto(
                    entitySpeaker.Id, entitySpeaker.FullName))
            );
        }
    }
}