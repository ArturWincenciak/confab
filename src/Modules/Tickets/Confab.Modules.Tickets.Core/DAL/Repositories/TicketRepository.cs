using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Confab.Modules.Tickets.Core.Entities;
using Confab.Modules.Tickets.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Confab.Modules.Tickets.Core.DAL.Repositories
{
    internal class TicketRepository : ITicketRepository
    {
        private readonly TicketsDbContext _context;
        private readonly DbSet<Ticket> _tickets;

        public TicketRepository(TicketsDbContext context)
        {
            _context = context;
            _tickets = _context.Tickets;
        }

        public Task<Ticket> GetAsync(Guid conferenceId, Guid userId)
        {
            return _tickets.SingleOrDefaultAsync(x => x.ConferenceId == conferenceId && x.UserId == userId);
        }

        public Task<int> GetCountForConferenceAsync(Guid conferenceId)
        {
            return _tickets.CountAsync(x => x.ConferenceId == conferenceId);
        }

        public async Task<IReadOnlyList<Ticket>> GetForUserIncludingConferenceAsync(Guid userId)
        {
            return await _tickets
                .Include(x => x.Conference)
                .Where(x => x.UserId == userId)
                .ToListAsync();
        }

        public Task<Ticket> GetAsync(string code)
        {
            return _tickets.SingleOrDefaultAsync(x => x.Code == code);
        }

        public async Task AddAsync(Ticket entity)
        {
            _tickets.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task AddManyAsync(IEnumerable<Ticket> entities)
        {
            _tickets.AddRange(entities);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Ticket entity)
        {
            _tickets.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Ticket entity)
        {
            _tickets.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}