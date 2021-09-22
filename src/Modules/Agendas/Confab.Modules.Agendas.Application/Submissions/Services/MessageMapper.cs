using System.Collections.Generic;
using System.Linq;
using Confab.Modules.Agendas.Application.Submissions.Events;
using Confab.Shared.Abstractions.Kernel;
using Confab.Shared.Abstractions.Messaging;

namespace Confab.Modules.Agendas.Application.Submissions.Services
{
    internal class MessageMapper : IMessageMapper
    {
        public IMessage Map(IDomainEvent @event)
        {
            return @event switch
            {
                Domain.Submissions.Events.SubmissionAdded e => new SubmissionAdded(e.Submission.Id),
                Domain.Submissions.Events.SubmissionApproved e => new SubmissionApproved(e.Id),
                Domain.Submissions.Events.SubmissionRejected e => new SubmissionRejected(e.Id),
                _ => null
            };
        }

        public IEnumerable<IMessage> Map(IEnumerable<IDomainEvent> events)
        {
            return events.Select(Map);
        }
    }
}