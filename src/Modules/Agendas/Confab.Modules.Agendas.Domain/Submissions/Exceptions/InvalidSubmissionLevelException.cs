using System;
using Confab.Shared.Kernel.Exceptions;

namespace Confab.Modules.Agendas.Domain.Submissions.Exceptions
{
    internal sealed class InvalidSubmissionLevelException : ConfabException
    {
        public InvalidSubmissionLevelException(Guid submissionId)
            : base($"Submission with ID: '{submissionId}' defines invalid level.")
        {
            SubmissionId = submissionId;
        }

        public Guid SubmissionId { get; }
    }
}