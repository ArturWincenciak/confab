using System.Linq;
using System.Threading.Tasks;
using Confab.Modules.Agendas.Application.Submissions.Exceptions;
using Confab.Modules.Agendas.Application.Submissions.Repositories;
using Confab.Modules.Agendas.Application.Submissions.Services;
using Confab.Shared.Abstractions.Commands;
using Confab.Shared.Abstractions.Messaging;
using Confab.Shared.Kernel;

namespace Confab.Modules.Agendas.Application.Submissions.Commands.Handlers;

internal sealed class ApproveSubmissionHandler : ICommandHandler<ApproveSubmission>
{
    private readonly IDomainEventDispatcher _domainEventDispatcher;
    private readonly IMessageBroker _messageBroker;
    private readonly IMessageMapper _messageMapper;
    private readonly ISubmissionRepository _submissionRepository;

    public ApproveSubmissionHandler(ISubmissionRepository submissionRepository,
        IDomainEventDispatcher domainEventDispatcher, IMessageBroker messageBroker, IMessageMapper messageMapper)
    {
        _submissionRepository = submissionRepository;
        _domainEventDispatcher = domainEventDispatcher;
        _messageBroker = messageBroker;
        _messageMapper = messageMapper;
    }

    public async Task HandleAsync(ApproveSubmission command)
    {
        var submission = await _submissionRepository.GetAsync(command.Id);
        if (submission is null)
            throw new SubmissionNotFoundException(command.Id);

        submission.Approve();
        await _submissionRepository.UpdateAsync(submission);

        await _domainEventDispatcher.SendAsync(submission.Events.ToArray());
        var integrationEvents = _messageMapper.Map(submission.Events);
        await _messageBroker.PublishAsync(integrationEvents.ToArray());
    }
}