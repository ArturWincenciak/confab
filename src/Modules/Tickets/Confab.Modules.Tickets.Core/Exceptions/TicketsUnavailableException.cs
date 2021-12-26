using System;
using Confab.Shared.Kernel.Exceptions;

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
}