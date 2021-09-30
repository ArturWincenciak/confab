using System;
using System.Collections;
using System.Collections.Generic;
using Confab.Shared.Abstractions.Queries;

namespace Confab.Modules.Agendas.Application.Agendas.Queries
{
    public sealed record BrowsAgendaItems(Guid ConferenceId) : IQuery<BrowsAgendaItems.AgendaItemsDto>
    {
        //todo: sprawdz czy deserializator ogarnie ze to jest IEnumerable i zwróci kolekcę :)
        public sealed class AgendaItemsDto : IEnumerable<AgendaItemDto>, IQueryResult
        {
            private readonly IEnumerable<AgendaItemDto> _agendaItem;

            public AgendaItemsDto(IEnumerable<AgendaItemDto> agendaItem)
            {
                _agendaItem = agendaItem;
            }

            public IEnumerator<AgendaItemDto> GetEnumerator()
            {
                return _agendaItem.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        public sealed record AgendaItemDto(Guid Id, Guid ConferenceId, string Title, string Description, int Level,
            IEnumerable<string> Tags, IEnumerable<SpeakerDto> Speakers);

        public sealed record SpeakerDto(Guid Id, string FullName);
    }
}