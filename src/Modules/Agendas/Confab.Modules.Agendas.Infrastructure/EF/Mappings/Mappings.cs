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
                slot.Id,
                slot.From,
                slot.To,
                Type = AgendaSlotType.Regular,
                ParticipantsLimit = slot.ParticipantLimit,
                AgendaItem = new
                {
                    slot.AgendaItem.Id,
                    slot.AgendaItem.ConferenceId,
                    slot.AgendaItem.Title,
                    slot.AgendaItem.Description,
                    slot.AgendaItem.Level,
                    slot.AgendaItem.Tags,
                    Speakers = slot.AgendaItem.Speakers.Select(x => new
                    {
                        x.Id, x.FullName
                    })
                }
            };
        }

        private static object AsDto(PlaceholderAgendaSlot slot)
        {
            return new
            {
                slot.Id,
                slot.From,
                slot.To,
                Type = AgendaSlotType.Placeholder,
                slot.Placeholder
            };
        }
    }
}