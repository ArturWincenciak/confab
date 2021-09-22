using System;
using Confab.Shared.Abstractions.Events;

namespace Confab.Modules.Agendas.Application.Submissions.Events
{
    internal sealed record SubmissionAdded(Guid Id) : IEvent;
}