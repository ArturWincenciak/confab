using Confab.Shared.Kernel.Exceptions;
using Confab.Shared.Kernel.Types.Base;

namespace Confab.Modules.Agendas.Domain.Submissions.Exceptions;

internal sealed class EmptySubmissionDescriptionException : ConfabException
{
    public AggregateId SubmissionId { get; }

    public EmptySubmissionDescriptionException(AggregateId submissionId)
        : base($"Submission with ID: '{submissionId}' defines empty description.") =>
        SubmissionId = submissionId;
}