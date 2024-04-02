using System;
using System.Collections.Generic;
using Confab.Shared.Abstractions.Queries;

namespace Confab.Modules.Agendas.Application.Agendas.Queries;

public sealed record GetAgendaItem(Guid ConferenceId, Guid Id) : IRequestMessage<GetAgendaItem.Result>
{
    public sealed record Result(Guid Id,
        Guid ConferenceId,
        string Title,
        string Description,
        int Level,
        IEnumerable<string> Tags,
        IEnumerable<Result.SpeakerDto> Speakers) : IResponseMessage
    {
        public sealed record SpeakerDto(Guid Id, string FullName);
    }
}