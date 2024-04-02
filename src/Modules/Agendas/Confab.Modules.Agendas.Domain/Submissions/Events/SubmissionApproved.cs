using Confab.Modules.Agendas.Domain.Submissions.Entities;
using Confab.Shared.Kernel;

namespace Confab.Modules.Agendas.Domain.Submissions.Events;

public record SubmissionApproved(Submission Submission) : IDomainEvent;