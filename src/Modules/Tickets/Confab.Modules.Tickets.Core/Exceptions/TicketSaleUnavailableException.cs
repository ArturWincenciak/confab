using System;
using Confab.Shared.Kernel.Exceptions;

namespace Confab.Modules.Tickets.Core.Exceptions;

internal class TicketSaleUnavailableException : ConfabException
{
    public Guid ConferenceId { get; }

    public TicketSaleUnavailableException(Guid conferenceId)
        : base("Ticket sale for the conference is unavailable.") =>
        ConferenceId = conferenceId;
}