using System;
using System.Collections;
using System.Collections.Generic;
using Confab.Shared.Abstractions.Queries;

namespace Confab.Modules.Agendas.Application.Agendas.Queries;

public sealed record BrowsAgendaItems(Guid ConferenceId) : IRequestMessage<BrowsAgendaItems.Result>
{
    public sealed class Result : IEnumerable<Result.AgendaItemDto>,
        IResponseMessage
    {
        private readonly IEnumerable<AgendaItemDto> _agendaItem;

        public Result(IEnumerable<AgendaItemDto> agendaItem) =>
            _agendaItem = agendaItem;

        public IEnumerator<AgendaItemDto> GetEnumerator() =>
            _agendaItem.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() =>
            GetEnumerator();

        public sealed record AgendaItemDto(Guid Id,
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