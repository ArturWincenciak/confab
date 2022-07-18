using System;
using Confab.Shared.Abstractions.Events;

namespace Confab.Modules.Saga.Messages
{
    internal record SpeakerCreated(Guid Id, string FullName) : IEvent;
}