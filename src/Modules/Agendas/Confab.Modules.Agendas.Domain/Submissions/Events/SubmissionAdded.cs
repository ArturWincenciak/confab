using Confab.Modules.Agendas.Domain.Submissions.Entities;
using Confab.Shared.Kernel;

namespace Confab.Modules.Agendas.Domain.Submissions.Events
{
    public record SubmissionAdded(Submission Submission) : IDomainEvent;
}