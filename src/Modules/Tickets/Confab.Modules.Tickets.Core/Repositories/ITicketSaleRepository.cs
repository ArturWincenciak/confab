using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Confab.Modules.Tickets.Core.Entities;

namespace Confab.Modules.Tickets.Core.Repositories
{
    internal interface ITicketSaleRepository
    {
        Task<TicketSale> GetIncludingTicketsAsync(Guid id);
        Task<TicketSale> GetCurrentForConferencesIncludingTicketsAsync(Guid conferenceId, DateTime now);
        Task<IReadOnlyList<TicketSale>> BrowseForConferenceAsync(Guid conferenceId);
        Task AddAsync(TicketSale entity);
        Task UpdateAsync(TicketSale entity);
        Task DeleteAsync(TicketSale entity);
    }
}