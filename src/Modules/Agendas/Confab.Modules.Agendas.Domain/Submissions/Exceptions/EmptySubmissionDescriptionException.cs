using Confab.Shared.Abstractions.Exceptions;
using Confab.Shared.Abstractions.Kernel.Types.Base;

namespace Confab.Modules.Agendas.Domain.Submissions.Exceptions
{
    internal sealed class EmptySubmissionDescriptionException : ConfabException
    {
        public EmptySubmissionDescriptionException(AggregateId submissionId)
            : base($"Submission with ID: '{submissionId}' defines empty description.")
        {
            SubmissionId = submissionId;
        }

        public AggregateId SubmissionId { get; }
    }
}