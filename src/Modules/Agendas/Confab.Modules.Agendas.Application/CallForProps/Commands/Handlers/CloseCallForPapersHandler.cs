using System.Threading.Tasks;
using Confab.Modules.Agendas.Application.CallForProps.Events;
using Confab.Modules.Agendas.Application.CallForProps.Exceptions;
using Confab.Modules.Agendas.Application.Submissions.Repositories;
using Confab.Shared.Abstractions.Commands;
using Confab.Shared.Abstractions.Messaging;

namespace Confab.Modules.Agendas.Application.CallForProps.Commands.Handlers
{
    internal sealed class CloseCallForPapersHandler : ICommandHandler<CloseCallForPapers>
    {
        private readonly ICallForPapersRepository _repository;
        private readonly IMessageBroker _messageBroker;

        public CloseCallForPapersHandler(ICallForPapersRepository repository, IMessageBroker messageBroker)
        {
            _repository = repository;
            _messageBroker = messageBroker;
        }

        public async Task HandleAsync(CloseCallForPapers command)
        {
            var callForPapers = await _repository.GetAsync(command.ConferenceId);
            if (callForPapers is null)
                throw new CallForPapersNotFoundExistsException(command.ConferenceId);

            callForPapers.Close();
            await _repository.UpdateAsync(callForPapers);

            await _messageBroker.PublishAsync(new CallForPaperClosed(command.ConferenceId));
        }
    }
}