using System.Threading.Tasks;
using Confab.Modules.Agendas.Application.CallForProps.Events;
using Confab.Modules.Agendas.Application.CallForProps.Exceptions;
using Confab.Modules.Agendas.Application.CallForProps.Repositories;
using Confab.Shared.Abstractions.Commands;
using Confab.Shared.Abstractions.Messaging;

namespace Confab.Modules.Agendas.Application.CallForProps.Commands.Handlers;

internal sealed class OpenCallForPapersHandler : ICommandHandler<OpenCallForPapers>
{
    private readonly IMessageBroker _messageBroker;
    private readonly ICallForPapersRepository _repository;

    public OpenCallForPapersHandler(ICallForPapersRepository repository, IMessageBroker messageBroker)
    {
        _repository = repository;
        _messageBroker = messageBroker;
    }

    public async Task HandleAsync(OpenCallForPapers command)
    {
        var callForPapers = await _repository.GetAsync(command.ConferenceId);
        if (callForPapers is null)
            throw new CallForPapersNotFoundExistsException(command.ConferenceId);

        callForPapers.Open();
        await _repository.UpdateAsync(callForPapers);

        await _messageBroker.PublishAsync(new CallForPaperOpened(command.ConferenceId));
    }
}