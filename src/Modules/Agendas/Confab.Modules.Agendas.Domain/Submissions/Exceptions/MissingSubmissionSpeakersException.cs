using System;
using Confab.Shared.Kernel.Exceptions;

namespace Confab.Modules.Agendas.Domain.Submissions.Exceptions
{
    internal sealed class MissingSubmissionSpeakersException : ConfabException
    {
        public MissingSubmissionSpeakersException(Guid submissionId)
            : base($"Submission with ID: '{submissionId}' has missing speakers.")
        {
            SubmissionId = submissionId;
        }

        public Guid SubmissionId { get; }
    }
}