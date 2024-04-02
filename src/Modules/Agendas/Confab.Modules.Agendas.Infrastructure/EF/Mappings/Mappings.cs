using System.Collections.Generic;
using System.Linq;
using Confab.Modules.Agendas.Application.Agendas.Types;
using Confab.Modules.Agendas.Domain.Agendas.Entities;
using Confab.Modules.Agendas.Domain.Submissions.Entities;

namespace Confab.Modules.Agendas.Infrastructure.EF.Mappings;

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

    private static object AsDto(PlaceholderAgendaSlot slot) =>
        new
        {
            slot.Id,
            slot.From,
            slot.To,
            Type = AgendaSlotType.Placeholder,
            slot.Placeholder
        };

    private static object AsDto(RegularAgendaSlot slot) =>
        new
        {
            Id = slot.Id.Value,
            slot.From,
            slot.To,
            Type = AgendaSlotType.Regular,
            ParticipantsLimit = slot.ParticipantLimit,
            AgendaItem = slot.AgendaItem is null
                ? null
                : new
                {
                    Id = slot.AgendaItem.Id.Value,
                    slot.AgendaItem.ConferenceId,
                    slot.AgendaItem.Title,
                    slot.AgendaItem.Description,
                    slot.AgendaItem.Level,
                    slot.AgendaItem.Tags,
                    Speakers = AsDto(slot.AgendaItem?.Speakers)
                }
        };

    private static IEnumerable<object> AsDto(IEnumerable<Speaker> speakers)
    {
        return speakers?.Select(x => new
               {
                   Id = x.Id.Value, x.FullName
               }) ??
               Enumerable.Empty<object>();
    }
}