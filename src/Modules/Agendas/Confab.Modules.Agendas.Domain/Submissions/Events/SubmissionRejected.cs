using System;
using Confab.Shared.Abstractions.Kernel;

namespace Confab.Modules.Agendas.Domain.Submissions.Events
{
    internal record SubmissionRejected(Guid Id) : IDomainEvent;
}