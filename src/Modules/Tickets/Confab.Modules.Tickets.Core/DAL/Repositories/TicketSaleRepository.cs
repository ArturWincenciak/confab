using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Confab.Modules.Tickets.Core.Entities;
using Confab.Modules.Tickets.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Confab.Modules.Tickets.Core.DAL.Repositories
{
    internal class TicketSaleRepository : ITicketSaleRepository
    {
        private readonly TicketsDbContext _context;
        private readonly DbSet<TicketSale> _ticketSales;

        public TicketSaleRepository(TicketsDbContext context)
        {
            _context = context;
            _ticketSales = _context.TicketSales;
        }

        public Task<TicketSale> GetIncludingTicketsAsync(Guid id)
        {
            return _ticketSales
                .Include(x => x.Tickets)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public Task<TicketSale> GetCurrentForConferencesIncludingTicketsAsync(Guid conferenceId, DateTime now)
        {
            return _ticketSales
                .Where(x => x.ConferenceId == conferenceId)
                .OrderBy(x => x.From)
                .Include(x => x.Tickets)
                .LastOrDefaultAsync(x => x.From <= now && x.To >= now);
        }

        public async Task<IReadOnlyList<TicketSale>> BrowseForConferenceAsync(Guid conferenceId)
        {
            return await _ticketSales
                .AsNoTracking()
                .Where(x => x.ConferenceId == conferenceId)
                .Include(x => x.Tickets)
                .ToListAsync();
        }

        public async Task AddAsync(TicketSale entity)
        {
            _ticketSales.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TicketSale entity)
        {
            _ticketSales.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TicketSale entity)
        {
            _ticketSales.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}