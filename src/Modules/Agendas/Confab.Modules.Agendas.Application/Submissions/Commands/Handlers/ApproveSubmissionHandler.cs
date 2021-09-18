using System.Threading.Tasks;
using Confab.Modules.Agendas.Application.Submissions.Exceptions;
using Confab.Modules.Agendas.Domain.Submissions.Repositories;
using Confab.Shared.Abstractions.Commands;

namespace Confab.Modules.Agendas.Application.Submissions.Commands.Handlers
{
    internal sealed class ApproveSubmissionHandler : ICommandHandler<ApproveSubmission, bool>
    {
        private readonly ISubmissionRepository _submissionRepository;

        public ApproveSubmissionHandler(ISubmissionRepository submissionRepository)
        {
            _submissionRepository = submissionRepository;
        }

        public async Task<bool> HandleAsync(ApproveSubmission command)
        {
            var submission = await _submissionRepository.GetAsync(command.Id);
            if (submission is null)
                throw new SubmissionNotFoundException(command.Id);

            submission.Approve();

            await _submissionRepository.UpdateAsync(submission);

            return true; //todo: temp
        }
    }
}