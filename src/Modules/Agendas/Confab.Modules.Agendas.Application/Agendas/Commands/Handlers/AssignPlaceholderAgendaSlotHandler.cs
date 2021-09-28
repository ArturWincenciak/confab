using System;
using System.Threading.Tasks;
using Confab.Modules.Agendas.Application.Agendas.Events;
using Confab.Modules.Agendas.Application.Agendas.Exceptions;
using Confab.Modules.Agendas.Domain.Agendas.Repositories;
using Confab.Modules.Agendas.Domain.Agendas.Services;
using Confab.Shared.Abstractions.Commands;
using Confab.Shared.Abstractions.Messaging;

namespace Confab.Modules.Agendas.Application.Agendas.Commands.Handlers
{
    internal sealed class AssignPlaceholderAgendaSlotHandler : ICommandHandler<AssignPlaceholderAgendaSlot>
    {
        private readonly IAgendaTrackRepository _repository;
        private readonly IMessageBroker _messageBroker;

        public AssignPlaceholderAgendaSlotHandler(IAgendaTrackRepository repository, IMessageBroker messageBroker)
        {
            _repository = repository;
            _messageBroker = messageBroker;
        }

        public async Task HandleAsync(AssignPlaceholderAgendaSlot command)
        {
            var agendaTrack = await _repository.GetAsync(command.AgendaTrackId);
            if (agendaTrack is null)
                throw new AgendaTrackNotFoundException(command.AgendaTrackId);

            agendaTrack.ChangeSlotPlaceholder(command.AgendaSlotId, command.Placeholder);

            await _repository.UpdateAsync(agendaTrack);

            var @event = new PlaceholderAssignedToAgendaSlot(command.AgendaSlotId, command.Placeholder);
            await _messageBroker.PublishAsync(@event);
        }
    }
}