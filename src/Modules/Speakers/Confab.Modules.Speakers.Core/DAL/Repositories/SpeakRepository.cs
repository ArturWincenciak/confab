using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Confab.Modules.Speakers.Core.Entities;
using Confab.Modules.Speakers.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Confab.Modules.Speakers.Core.DAL.Repositories
{
    internal class SpeakRepository : ISpeakerRepository
    {
        private readonly SpeakersDbContext _dbContext;
        private readonly DbSet<Speaker> _speakers;

        public SpeakRepository(SpeakersDbContext dbContext)
        {
            _dbContext = dbContext;
            _speakers = _dbContext.Speakers;
        }

        public async Task AddAsync(Speaker speaker)
        {
            await _speakers.AddAsync(speaker);
            await _dbContext.SaveChangesAsync();
        }

        public Task<Speaker> GetAsync(Guid id)
        {
            return _speakers.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IReadOnlyList<Speaker>> BrowseAsync()
        {
            return await _speakers.ToListAsync();
        }

        public async Task UpdateAsync(Speaker speaker)
        {
            _speakers.Update(speaker);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Speaker speaker)
        {
            _speakers.Remove(speaker);
            await _dbContext.SaveChangesAsync();
        }
    }
}
