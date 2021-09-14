using System;
using Confab.Shared.Abstractions.Events;

namespace Confab.Modules.Speakers.Core.Events
{
    internal record SpeakerCreated(Guid Id, string FullName) : IEvent;
}