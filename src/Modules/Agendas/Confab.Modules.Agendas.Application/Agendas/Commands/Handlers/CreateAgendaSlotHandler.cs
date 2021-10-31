using System;
using System.Threading.Tasks;
using Confab.Modules.Agendas.Application.Agendas.Events;
using Confab.Modules.Agendas.Application.Agendas.Exceptions;
using Confab.Modules.Agendas.Application.Agendas.Types;
using Confab.Modules.Agendas.Domain.Agendas.Repositories;
using Confab.Shared.Abstractions.Commands;
using Confab.Shared.Abstractions.Kernel.Types.Base;
using Confab.Shared.Abstractions.Messaging;

namespace Confab.Modules.Agendas.Application.Agendas.Commands.Handlers
{
    internal sealed class CreateAgendaSlotHandler : ICommandHandler<CreateAgendaSlot, CreateAgendaSlot.AgendaSlotId>
    {
        private readonly IAgendaTrackRepository _agendaTrackRepository;
        private readonly IMessageBroker _messageBroker;

        public CreateAgendaSlotHandler(IAgendaTrackRepository agendaTrackRepository, IMessageBroker messageBroker)
        {
            _agendaTrackRepository = agendaTrackRepository;
            _messageBroker = messageBroker;
        }

        public async Task<CreateAgendaSlot.AgendaSlotId> HandleAsync(CreateAgendaSlot command)
        {
            var agendaTrack = await _agendaTrackRepository.GetAsync(command.AgendaTrackId);
            if (agendaTrack is null)
                throw new AgendaTrackNotFoundException(command.AgendaTrackId);

            EntityId createdSlotId = Guid.Empty;

            if (command.Type is AgendaSlotType.Regular)
                createdSlotId = agendaTrack.AddRegularSlot(command.From, command.To, command.ParticipantsLimit);
            else if (command.Type is AgendaSlotType.Placeholder)
                createdSlotId = agendaTrack.AddPlaceholderSlot(command.From, command.To);

            await _agendaTrackRepository.UpdateAsync(agendaTrack);

            await _messageBroker.PublishAsync(new AgendaTrackUpdated(agendaTrack.Id));

            return new CreateAgendaSlot.AgendaSlotId(createdSlotId);
        }
    }
}