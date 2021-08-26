using System;

namespace Confab.Modules.Tickets.Core.Exceptions
{
    internal class TicketAlreadyPurchasedException : Exception
    {
        public TicketAlreadyPurchasedException(Guid conferenceId, Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}