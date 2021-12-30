using System;
using Confab.Shared.Abstractions.Queries;

namespace Confab.Modules.Attendances.Application.Clients.Agendas.DTO
{
    public record RegularAgendaSlotDto(
        Guid Id,
        DateTime From,
        DateTime To,
        string Type,
        int? ParticipantsLimit,
        AgendaItemDto AgendaItem) : IModuleResponse;
}