using System;
using System.Collections.Generic;
using Confab.Shared.Abstractions.Queries;

namespace Confab.Modules.Agendas.Application.Agendas.Queries;

public sealed record GetRegularAgendaSlot(Guid AgendaItemId) : IRequestMessage<GetRegularAgendaSlot.Result>,
    IModuleRequest
{
    public sealed record Result(
        Guid Id,
        DateTime From,
        DateTime To,
        string Type,
        int? ParticipantLimit,
        Result.AgendaItemDto AgendaItem)
        : IResponseMessage,
            IModuleResponse
    {
        public sealed record AgendaItemDto(
            Guid Id,
            Guid ConferenceId,
            string Title,
            string Description,
            int Level,
            IEnumerable<string> Tags,
            IEnumerable<AgendaItemDto.SpeakerDto> Speakers)
        {
            public sealed record SpeakerDto(Guid Id, string FullName);
        }
    }
}