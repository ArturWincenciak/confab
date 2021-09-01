using System;
using Confab.Shared.Abstractions.Events;

namespace Confab.Modules.Conferences.Messages.Events
{
    public record ConferenceCreated(Guid Id, string Name, int? ParticipantsLimit, DateTime From, DateTime To) : IEvent;
}