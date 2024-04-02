using System;
using Confab.Modules.Tickets.Core.Entities;

namespace Confab.Modules.Tickets.Core.Services;

internal interface ITicketGenerator
{
    Ticket Generate(Guid conferenceId, Guid ticketSaleId, decimal? price);
}