using System.Linq;
using System.Threading.Tasks;
using Confab.Modules.Agendas.Application.Submissions.Exceptions;
using Confab.Modules.Agendas.Domain.Submissions.Repositories;
using Confab.Shared.Abstractions.Commands;
using Confab.Shared.Abstractions.Kernel;

namespace Confab.Modules.Agendas.Application.Submissions.Commands.Handlers
{
    internal sealed class RejectSubmissionHandler : ICommandHandler<RejectSubmission>
    {
        private readonly IDomainEventDispatcher _domainEventDispatcher;
        private readonly ISubmissionRepository _submissionRepository;

        public RejectSubmissionHandler(ISubmissionRepository submissionRepository,
            IDomainEventDispatcher domainEventDispatcher)
        {
            _submissionRepository = submissionRepository;
            _domainEventDispatcher = domainEventDispatcher;
        }

        public async Task HandleAsync(RejectSubmission command)
        {
            var submission = await _submissionRepository.GetAsync(command.Id);
            if (submission is null)
                throw new SubmissionNotFoundException(command.Id);

            submission.Reject();
            await _submissionRepository.UpdateAsync(submission);
            await _domainEventDispatcher.SendAsync(submission.Events.ToArray());
        }
    }
}