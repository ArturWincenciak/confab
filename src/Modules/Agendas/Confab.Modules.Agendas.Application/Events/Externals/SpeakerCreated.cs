using System;
using Confab.Shared.Abstractions.Events;

namespace Confab.Modules.Agendas.Application.Events.Externals
{
    internal record SpeakerCreated(Guid Id, string FullName) : IEvent;
}
