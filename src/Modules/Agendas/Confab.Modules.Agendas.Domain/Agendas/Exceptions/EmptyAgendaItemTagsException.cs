using Confab.Shared.Kernel.Exceptions;
using Confab.Shared.Kernel.Types.Base;

namespace Confab.Modules.Agendas.Domain.Agendas.Exceptions;

internal class EmptyAgendaItemTagsException : ConfabException
{
    public AggregateId SubmissionId { get; }

    public EmptyAgendaItemTagsException(AggregateId submissionId)
        : base($"Agenda Item with Submission Id: '{submissionId}' defines empty tags.") =>
        SubmissionId = submissionId;
}