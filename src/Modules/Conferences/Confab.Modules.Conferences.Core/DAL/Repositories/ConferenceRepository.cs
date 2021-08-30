using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Confab.Modules.Conferences.Core.Entities;
using Confab.Modules.Conferences.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Confab.Modules.Conferences.Core.DAL.Repositories
{
    internal class ConferenceRepository : IConferenceRepository
    {
        private readonly DbSet<Conference> _conferences;
        private readonly ConferencesDbContext _context;

        public ConferenceRepository(ConferencesDbContext context)
        {
            _context = context;
            _conferences = _context.Conferences;
        }

        public async Task AddAsync(Conference conference)
        {
            await _conferences.AddAsync(conference);
            await _context.SaveChangesAsync();
        }

        public Task<Conference> GetAsync(Guid id)
        {
            return _conferences
                .Include(c => c.Host)
                .SingleOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IReadOnlyList<Conference>> BrowseAsync()
        {
            return await _conferences
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task UpdateAsync(Conference conference)
        {
            _conferences.Update(conference);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Conference conference)
        {
            _conferences.Remove(conference);
            await _context.SaveChangesAsync();
        }
    }
}