using System.Collections.Generic;
using System.Linq;
using Confab.Modules.Agendas.Domain.Submissions.Events;
using Confab.Shared.Abstractions.Messaging;
using Confab.Shared.Kernel;

namespace Confab.Modules.Agendas.Application.Submissions.Services
{
    internal class MessageMapper : IMessageMapper
    {
        public IMessage Map(IDomainEvent @event)
        {
            return @event switch
            {
                SubmissionAdded e => new Events.SubmissionAdded(e.Submission.Id),
                SubmissionApproved e => new Events.SubmissionApproved(e.Submission.Id),
                SubmissionRejected e => new Events.SubmissionRejected(e.Id),
                _ => null
            };
        }

        public IEnumerable<IMessage> Map(IEnumerable<IDomainEvent> events)
        {
            return events.Select(Map);
        }
    }
}