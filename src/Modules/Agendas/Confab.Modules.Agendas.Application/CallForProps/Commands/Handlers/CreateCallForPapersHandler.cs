﻿using System.Threading.Tasks;
using Confab.Modules.Agendas.Application.CallForProps.Events;
using Confab.Modules.Agendas.Application.CallForProps.Exceptions;
using Confab.Modules.Agendas.Domain.CallForPaper.Entities;
using Confab.Modules.Agendas.Domain.CallForPaper.Repositories;
using Confab.Shared.Abstractions.Commands;
using Confab.Shared.Abstractions.Messaging;

namespace Confab.Modules.Agendas.Application.CallForProps.Commands.Handlers
{
    internal sealed class CreateCallForPapersHandler : ICommandHandler<CreateCallForPapers>
    {
        private readonly ICallForPapersRepository _repository;
        private readonly IMessageBroker _messageBroker;

        public CreateCallForPapersHandler(ICallForPapersRepository repository, IMessageBroker messageBroker)
        {
            _repository = repository;
            _messageBroker = messageBroker;
        }

        public async Task HandleAsync(CreateCallForPapers command)
        {
            if (await _repository.ExistsAsync(command.ConferenceId))
                throw new CallForPapersAlreadyExistsException(command.ConferenceId);

            var callForPapers = CallForPapers.Create(command.ConferenceId, command.From, command.To);
            await _repository.AddAsync(callForPapers);

            await _messageBroker.PublishAsync(new CallForPaperCreated(callForPapers.Id));
        }
    }
}