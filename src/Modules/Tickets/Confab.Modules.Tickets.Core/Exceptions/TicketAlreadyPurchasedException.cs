using System;
using Confab.Shared.Kernel.Exceptions;

namespace Confab.Modules.Tickets.Core.Exceptions;

internal class TicketAlreadyPurchasedException : ConfabException
{
    public Guid ConferenceId { get; }
    public Guid UserId { get; }

    public TicketAlreadyPurchasedException(Guid conferenceId, Guid userId)
        : base("Ticket for the conference has been already purchased.")
    {
        ConferenceId = conferenceId;
        UserId = userId;
    }
}