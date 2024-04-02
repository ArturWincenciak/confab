﻿using System.Threading.Tasks;
using Confab.Modules.Agendas.Application.Agendas.Events;
using Confab.Modules.Agendas.Application.Agendas.Exceptions;
using Confab.Modules.Agendas.Domain.Agendas.Repositories;
using Confab.Shared.Abstractions.Commands;
using Confab.Shared.Abstractions.Messaging;

namespace Confab.Modules.Agendas.Application.Agendas.Commands.Handlers;

internal class DeleteAgendaTrackHandler : ICommandHandler<DeleteAgendaTrack>
{
    private readonly IMessageBroker _messageBroker;
    private readonly IAgendaTrackRepository _repository;

    public DeleteAgendaTrackHandler(IAgendaTrackRepository repository, IMessageBroker messageBroker)
    {
        _repository = repository;
        _messageBroker = messageBroker;
    }

    public async Task HandleAsync(DeleteAgendaTrack command)
    {
        var agendaTrack = await _repository.GetAsync(command.Id);
        if (agendaTrack is null)
            throw new AgendaTrackNotFoundException(command.Id);

        await _repository.DeleteAsync(agendaTrack);
        await _messageBroker.PublishAsync(new AgendaTrackDeleted(command.Id));
    }
}