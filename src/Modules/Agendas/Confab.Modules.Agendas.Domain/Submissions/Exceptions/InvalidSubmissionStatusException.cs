using System;
using Confab.Shared.Kernel.Exceptions;

namespace Confab.Modules.Agendas.Domain.Submissions.Exceptions;

internal sealed class InvalidSubmissionStatusException : ConfabException
{
    public Guid SubmissionId { get; }

    public InvalidSubmissionStatusException(Guid submissionId, string desiredStatus, string currentStatus)
        : base($"Cannot change status if submission with ID: '{submissionId}' " +
               $"from '{currentStatus}' to '{desiredStatus}'.") =>
        SubmissionId = submissionId;
}