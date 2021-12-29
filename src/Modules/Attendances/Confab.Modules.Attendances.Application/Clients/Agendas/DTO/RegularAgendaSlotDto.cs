using Confab.Shared.Abstractions.Queries;

namespace Confab.Modules.Attendances.Application.Clients.Agendas.DTO
{
    //todo: change to record
    public class RegularAgendaSlotDto : AgendaSlotDto, IModuleResponse
    {
        public int? ParticipantsLimit { get; set; }
        public AgendaItemDto AgendaItem { get; set; }
    }
}