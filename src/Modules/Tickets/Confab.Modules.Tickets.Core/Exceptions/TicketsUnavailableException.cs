using System;
using Confab.Shared.Abstractions.Exceptions;

namespace Confab.Modules.Tickets.Core.Exceptions
{
    internal class TicketsUnavailableException : ConfabException
    {
        public TicketsUnavailableException(Guid conferenceId)
            : base("There are no available tickets for the conference.")
        {
            ConferenceId = conferenceId;
        }

        public Guid ConferenceId { get; }
    }

    internal class TooManyTicketsException : ConfabException
    {
        public TooManyTicketsException(Guid conferenceId)
            : base("Too many tickets would be generated for the conference.")
        {
            ConferenceId = conferenceId;
        }

        public Guid ConferenceId { get; }
    }
}