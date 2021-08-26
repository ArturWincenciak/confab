using System;

namespace Confab.Modules.Tickets.Core.DTO
{
    internal record TicketDto(string Code, decimal? Price, DateTime PurchasedAt, ConferenceDto Conference);
}