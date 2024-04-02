using System;

namespace Confab.Modules.Tickets.Core.DTO;

internal record TicketSaleInfoDto(string Name,
    ConferenceDto Conference,
    decimal? TicketPrice,
    int? TotalTicket,
    int? AvailableTickets,
    DateTime SaleFrom,
    DateTime SaleTo)
{
    public bool IsFree => !TicketPrice.HasValue;
    public bool UnlimitedTickets => !AvailableTickets.HasValue;

    public bool IsAvailable => SaleFrom <= DateTime.UtcNow &&
                               SaleTo >= DateTime.UtcNow &&
                               (UnlimitedTickets || AvailableTickets > 0);
}