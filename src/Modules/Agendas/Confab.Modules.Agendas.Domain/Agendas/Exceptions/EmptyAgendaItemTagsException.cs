using Confab.Shared.Abstractions.Kernel.Types.Base;
using Confab.Shared.Kernel.Exceptions;

namespace Confab.Modules.Agendas.Domain.Agendas.Exceptions
{
    internal class EmptyAgendaItemTagsException : ConfabException
    {
        public EmptyAgendaItemTagsException(AggregateId submissionId)
            : base($"Agenda Item with Submission Id: '{submissionId}' defines empty tags.")
        {
            SubmissionId = submissionId;
        }

        public AggregateId SubmissionId { get; }
    }
}