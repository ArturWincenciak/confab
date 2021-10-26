using System;
using System.Linq;
using System.Threading.Tasks;
using Confab.Modules.Agendas.Application.Agendas.Queries;
using Confab.Modules.Agendas.Application.Agendas.Types;
using Confab.Modules.Agendas.Domain.Agendas.Entities;
using Confab.Shared.Abstractions.Queries;
using Microsoft.EntityFrameworkCore;

namespace Confab.Modules.Agendas.Infrastructure.EF.Queries.Handlers
{
    internal sealed class GetRegularAgendaSlotHandler : IQueryHandler<GetRegularAgendaSlot, GetRegularAgendaSlot.Result>
    {
        private readonly DbSet<AgendaSlot> _agendaSlots;

        public GetRegularAgendaSlotHandler(AgendasDbContext context)
        {
            _agendaSlots = context.AgendaSlots;
        }

        public async Task<GetRegularAgendaSlot.Result> HandleAsync(GetRegularAgendaSlot query)
        {
            var slot = await _agendaSlots
                .AsNoTracking()
                .OfType<RegularAgendaSlot>()
                .Include(x => x.AgendaItem)
                .ThenInclude(x => x.Speakers)
                .SingleOrDefaultAsync(x => x.AgendaItem.Id == query.AgendaItemId);

            return slot is not null ? AsDto(slot) : null;
        }

        private static GetRegularAgendaSlot.Result AsDto(RegularAgendaSlot slot)
        {
            return new GetRegularAgendaSlot.Result
            (
                slot.Id,
                slot.From,
                slot.To,
                AgendaSlotType.Regular,
                slot.ParticipantLimit,
                new GetRegularAgendaSlot.Result.AgendaItemDto(
                    slot.AgendaItem.Id,
                    slot.AgendaItem.ConferenceId,
                    slot.AgendaItem.Title,
                    slot.AgendaItem.Description,
                    slot.AgendaItem.Level,
                    slot.AgendaItem.Tags,
                    slot.AgendaItem.Speakers
                        .Select(x => new GetRegularAgendaSlot.Result.AgendaItemDto.SpeakerDto(
                            x.Id, x.FullName)))

            );
        }
    }
}