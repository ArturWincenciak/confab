using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Confab.Modules.Agendas.Domain.Submissions.Entities;
using Confab.Modules.Agendas.Domain.Submissions.Repositories;
using Confab.Shared.Abstractions.Kernel.Types;
using Microsoft.EntityFrameworkCore;

namespace Confab.Modules.Agendas.Infrastructure.EF.Repositories
{
    internal sealed class SpeakerRepository : ISpeakerRepository
    {
        private readonly AgendasDbContext _dbContext;
        private readonly DbSet<Speaker> _speakers;

        public SpeakerRepository(AgendasDbContext dbContext)
        {
            _dbContext = dbContext;
            _speakers = dbContext.Speakers;
        }

        public Task<bool> ExistsAsync(AggregateId id)
        {
            return _speakers.AnyAsync(x => x.Id.Equals(x));
        }

        public async Task<IEnumerable<Speaker>> BrowseAsync(IEnumerable<AggregateId> ids)
        {
            return await _speakers.Where(x => ids.Contains(x.Id)).ToListAsync();
        }

        public async Task CreateAsync(Speaker entity)
        {
            _speakers.Add(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}