using System;
using System.Threading.Tasks;
using Confab.Modules.Tickets.Core.Entities;
using Confab.Modules.Tickets.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Confab.Modules.Tickets.Core.DAL.Repositories
{
    internal class ConferenceRepository : IConferenceRepository
    {
        private readonly TicketsDbContext _context;
        private readonly DbSet<Conference> _conferences;

        public ConferenceRepository(TicketsDbContext context)
        {
            _context = context;
            _conferences = _context.Conferences;
        }

        public Task<Conference> GetAsync(Guid id)
        {
            return _conferences.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddAsync(Conference entity)
        {
            _conferences.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Conference entity)
        {
            _conferences.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Conference entity)
        {
            _conferences.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}