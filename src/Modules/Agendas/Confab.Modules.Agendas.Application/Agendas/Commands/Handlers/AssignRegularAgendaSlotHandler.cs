using System.Threading.Tasks;
using Confab.Modules.Agendas.Application.Agendas.Events;
using Confab.Modules.Agendas.Application.Agendas.Exceptions;
using Confab.Modules.Agendas.Domain.Agendas.Repositories;
using Confab.Modules.Agendas.Domain.Agendas.Services;
using Confab.Shared.Abstractions.Commands;
using Confab.Shared.Abstractions.Messaging;

namespace Confab.Modules.Agendas.Application.Agendas.Commands.Handlers
{
    internal sealed class AssignRegularAgendaSlotHandler : ICommandHandler<AssignRegularAgendaSlot>
    {
        private readonly IAgendaTrackRepository _agendaTrackRepository;
        private readonly IAgendaTracksDomainService _agendaTracksDomainService;
        private readonly IMessageBroker _messageBroker;

        public AssignRegularAgendaSlotHandler(IAgendaTrackRepository agendaTrackRepository,
            IAgendaTracksDomainService agendaTracksDomainService, IMessageBroker messageBroker)
        {
            _agendaTrackRepository = agendaTrackRepository;
            _agendaTracksDomainService = agendaTracksDomainService;
            _messageBroker = messageBroker;
        }

        public async Task HandleAsync(AssignRegularAgendaSlot command)
        {
            var agendaTrack = await _agendaTrackRepository.GetAsync(command.AgendaTrackId);
            if (agendaTrack is null)
                throw new AgendaTrackNotFoundException(command.AgendaTrackId);

            await _agendaTracksDomainService.AssignAgendaItemAsync(agendaTrack, command.AgendaSlotId,
                command.AgendaItemId);

            await _agendaTrackRepository.UpdateAsync(agendaTrack);

            var @event = new AgendasItemAssignedToAgendaSlot(command.AgendaSlotId, command.AgendaItemId);
            await _messageBroker.PublishAsync(@event);
        }
    }
}