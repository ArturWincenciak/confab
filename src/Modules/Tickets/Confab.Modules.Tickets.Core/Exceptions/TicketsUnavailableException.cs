using System;
using Confab.Shared.Kernel.Exceptions;

namespace Confab.Modules.Tickets.Core.Exceptions;

internal class TicketsUnavailableException : ConfabException
{
    public Guid ConferenceId { get; }

    public TicketsUnavailableException(Guid conferenceId)
        : base("There are no available tickets for the conference.") =>
        ConferenceId = conferenceId;
}