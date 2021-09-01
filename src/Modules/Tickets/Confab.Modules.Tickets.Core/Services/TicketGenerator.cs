using System;
using Confab.Modules.Tickets.Core.Entities;
using Confab.Shared.Abstractions;

namespace Confab.Modules.Tickets.Core.Services
{
    internal class TicketGenerator : ITicketGenerator
    {
        private readonly IClock _clock;

        public TicketGenerator(IClock clock)
        {
            _clock = clock;
        }

        public Ticket Generate(Guid conferenceId, Guid ticketSaleId, decimal? price)
        {
            return new Ticket
            {
                Id = Guid.NewGuid(),
                TicketSaleId = ticketSaleId,
                ConferenceId = conferenceId,
                Code = Guid.NewGuid().ToString("N"),
                Price = price,
                CreatedAt = _clock.CurrentDate()
            };
        }
    }
}