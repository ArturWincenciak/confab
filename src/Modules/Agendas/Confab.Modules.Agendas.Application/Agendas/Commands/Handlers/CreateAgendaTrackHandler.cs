using System.Threading.Tasks;
using Confab.Modules.Agendas.Application.Agendas.Events;
using Confab.Modules.Agendas.Domain.Agendas.Entities;
using Confab.Modules.Agendas.Domain.Agendas.Repositories;
using Confab.Shared.Abstractions.Commands;
using Confab.Shared.Abstractions.Messaging;

namespace Confab.Modules.Agendas.Application.Agendas.Commands.Handlers
{
    internal sealed class CreateAgendaTrackHandler : ICommandHandler<CreateAgendaTrack>
    {
        private readonly IAgendaTrackRepository _repository;
        private readonly IMessageBroker _messageBroker;

        public CreateAgendaTrackHandler(IAgendaTrackRepository repository, IMessageBroker messageBroker)
        {
            _repository = repository;
            _messageBroker = messageBroker;
        }

        public async Task HandleAsync(CreateAgendaTrack command)
        {
            var newAgendaTrack = AgendaTrack.Create(command.ConferenceId, command.Name);
            await _repository.AddAsync(newAgendaTrack);
            await _messageBroker.PublishAsync(new AgendaTrackCreated(newAgendaTrack.Id));
        }
    }
}