using System;
using Confab.Shared.Kernel.Exceptions;

namespace Confab.Modules.Agendas.Domain.Submissions.Exceptions;

internal sealed class EmptySubmissionTitleException : ConfabException
{
    public Guid SubmissionId { get; }

    public EmptySubmissionTitleException(Guid submissionId)
        : base($"Submission with ID: '{submissionId}' defines empty title.") =>
        SubmissionId = submissionId;
}