using System;
using Confab.Shared.Abstractions.Exceptions;

namespace Confab.Modules.Tickets.Core.Exceptions
{
    internal class TicketSaleUnavailableException : ConfabException
    {
        public TicketSaleUnavailableException(Guid conferenceId)
            : base("Ticket sale for the conference is unavailable.")
        {
            ConferenceId = conferenceId;
        }

        public Guid ConferenceId { get; }
    }

    internal class TicketsUnavailableException : ConfabException
    {
        public TicketsUnavailableException(Guid conferenceId)
            : base("There are no available tickets for the conference.")
        {
            ConferenceId = conferenceId;
        }

        public Guid ConferenceId { get; }
    }
}