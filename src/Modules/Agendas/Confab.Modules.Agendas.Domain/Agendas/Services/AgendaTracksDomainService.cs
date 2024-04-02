using System.Linq;
using System.Threading.Tasks;
using Confab.Modules.Agendas.Domain.Agendas.Entities;
using Confab.Modules.Agendas.Domain.Agendas.Exceptions;
using Confab.Modules.Agendas.Domain.Agendas.Repositories;
using Confab.Shared.Kernel.Types;
using Confab.Shared.Kernel.Types.Base;

namespace Confab.Modules.Agendas.Domain.Agendas.Services;

internal class AgendaTracksDomainService : IAgendaTracksDomainService
{
    private readonly IAgendaItemRepository _agendaItemRepository;
    private readonly IAgendaTrackRepository _agendaTrackRepository;

    public AgendaTracksDomainService(IAgendaTrackRepository agendaTrackRepository,
        IAgendaItemRepository agendaItemRepository)
    {
        _agendaTrackRepository = agendaTrackRepository;
        _agendaItemRepository = agendaItemRepository;
    }

    public async Task AssignAgendaItemAsync(AgendaTrack agendaTrack, EntityId agendaSlotId,
        AggregateId agendaItemId)
    {
        var agendaTracks = await _agendaTrackRepository.BrowsAsync(agendaTrack.ConferenceId);
        var slotToAssign = agendaTrack.Slots
            .OfType<RegularAgendaSlot>()
            .SingleOrDefault(x => x.Id == agendaSlotId);

        if (slotToAssign is null)
            throw new AgendaSlotNotFoundException(agendaSlotId);

        var agendaItem = await _agendaItemRepository.GetAsync(agendaItemId);

        if (agendaItem is null)
            throw new AgendaItemNotFoundException(agendaItemId);

        var speakerIds = agendaItem.Speakers.Select(x => new SpeakerId(x.Id));
        var speakerItems = await _agendaItemRepository.BrowsAsync(speakerIds);
        var speakerItemsIds = speakerItems.Select(x => new EntityId(x.Id));

        var hasCollidingSpeakerSlots = agendaTracks
            .SelectMany(x => x.Slots)
            .OfType<RegularAgendaSlot>()
            .Any(x => speakerItemsIds.Contains(x.Id) && x.From < slotToAssign.To && x.From > slotToAssign.To);

        if (hasCollidingSpeakerSlots)
            throw new CollidingSpeakerAgendaSlotsException(agendaSlotId, agendaItemId);

        agendaTrack.ChangeSlotAgendaItem(agendaSlotId, agendaItem);
    }
}