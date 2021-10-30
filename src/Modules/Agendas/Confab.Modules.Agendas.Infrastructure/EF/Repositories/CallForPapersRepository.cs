using System.Threading.Tasks;
using Confab.Modules.Agendas.Application.CallForProps.Repositories;
using Confab.Modules.Agendas.Domain.CallForPaper.Entities;
using Confab.Shared.Abstractions.Kernel.Types;
using Microsoft.EntityFrameworkCore;

namespace Confab.Modules.Agendas.Infrastructure.EF.Repositories
{
    internal sealed class CallForPapersRepository : ICallForPapersRepository
    {
        private readonly DbSet<CallForPapers> _callForPapers;
        private readonly AgendasDbContext _dbContext;

        public CallForPapersRepository(AgendasDbContext context)
        {
            _dbContext = context;
            _callForPapers = context.CallForPapers;
        }

        public async Task<CallForPapers> GetAsync(ConferenceId conferenceId)
        {
            return await _callForPapers.SingleOrDefaultAsync(x => x.ConferenceId == conferenceId);
        }

        public async Task<bool> ExistsAsync(ConferenceId conferenceId)
        {
            return await _callForPapers.AnyAsync(x => x.ConferenceId == conferenceId);
        }

        public async Task AddAsync(CallForPapers entity)
        {
            _callForPapers.Add(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(CallForPapers entity)
        {
            _callForPapers.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}