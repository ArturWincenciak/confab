using System;
using Confab.Shared.Abstractions.Kernel;

namespace Confab.Modules.Agendas.Domain.Submissions.Events
{
    public record SubmissionRejected(Guid Id) : IDomainEvent;
}