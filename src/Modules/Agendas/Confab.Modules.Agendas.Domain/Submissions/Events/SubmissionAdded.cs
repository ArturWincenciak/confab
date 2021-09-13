using System;
using Confab.Modules.Agendas.Domain.Submissions.Entities;
using Confab.Shared.Abstractions.Kernel;

namespace Confab.Modules.Agendas.Domain.Submissions.Events
{
    internal record SubmissionAdded(Submission Submission) : IDomainEvent;

    internal record SubmissionApproved(Guid Id) : IDomainEvent;

    internal record SubmissionRejected(Guid Id) : IDomainEvent;
}