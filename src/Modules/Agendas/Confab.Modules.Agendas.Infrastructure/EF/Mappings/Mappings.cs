using System.Collections.Generic;
using System.Linq;
using Confab.Modules.Agendas.Application.Agendas.Types;
using Confab.Modules.Agendas.Domain.Agendas.Entities;

namespace Confab.Modules.Agendas.Infrastructure.EF.Mappings
{
    internal static class MappingsExtension
    {
        public static IEnumerable<object> AsDto(IEnumerable<AgendaSlot> slots)
        {
            var regularSlots = slots
                .OfType<RegularAgendaSlot>()
                .Select(AsDto);

            var placeholderSlots = slots
                .OfType<PlaceholderAgendaSlot>()
                .Select(AsDto);

            var result = new List<object>();
            result.AddRange(regularSlots);
            result.AddRange(placeholderSlots);

            return result;
        }

        private static object AsDto(RegularAgendaSlot slot)
        {
            return new
            {
                Id = slot.Id,
                From = slot.From,
                To = slot.To,
                Type = AgendaSlotType.Regular,
                ParticipantsLimit = slot.ParticipantLimit,
                AgendaItem = new
                {
                    Id = slot.AgendaItem.Id,
                    ConferenceId = slot.AgendaItem.ConferenceId,
                    Title = slot.AgendaItem.Title,
                    Description = slot.AgendaItem.Description,
                    Level = slot.AgendaItem.Level,
                    Tags = slot.AgendaItem.Tags,
                    Speakers = slot.AgendaItem.Speakers.Select(x => new
                    {
                        Id = x.Id,
                        FullName = x.FullName
                    })
                }
            };
        }

        private static object AsDto(PlaceholderAgendaSlot slot)
        {
            return new
            {
                Id = slot.Id,
                From = slot.From,
                To = slot.To,
                Type = AgendaSlotType.Placeholder,
                Placeholder = slot.Placeholder
            };
        }
    }
}