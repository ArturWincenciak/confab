﻿using System;
using Confab.Shared.Kernel.Exceptions;

namespace Confab.Modules.Agendas.Application.Submissions.Exceptions;

internal class SubmissionNotFoundException : ConfabException
{
    public Guid SubmissionId { get; }

    public SubmissionNotFoundException(Guid submissionId)
        : base($"Submission with ID '{submissionId}' was not found.") =>
        SubmissionId = submissionId;
}