using System;
using System.Collections.Generic;

namespace Confab.Modules.Attendances.Application.Clients.Agendas.DTO
{
    public record AgendaItemDto(
        Guid Id,
        Guid ConferenceId,
        DateTime From,
        DateTime To,
        string Title,
        string Description,
        int Level,
        IEnumerable<string> Tags);
}