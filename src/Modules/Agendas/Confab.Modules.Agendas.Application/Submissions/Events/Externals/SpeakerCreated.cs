using System;
using Confab.Shared.Abstractions.Events;

namespace Confab.Modules.Agendas.Application.Submissions.Events.Externals
{
    internal record SpeakerCreated(Guid Id, string FullName) : IEvent;
}
