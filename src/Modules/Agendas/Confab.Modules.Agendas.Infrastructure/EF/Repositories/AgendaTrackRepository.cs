using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Confab.Modules.Agendas.Domain.Agendas.Entities;
using Confab.Modules.Agendas.Domain.Agendas.Repositories;
using Confab.Shared.Kernel.Types;
using Confab.Shared.Kernel.Types.Base;
using Microsoft.EntityFrameworkCore;

namespace Confab.Modules.Agendas.Infrastructure.EF.Repositories;

internal sealed class AgendaTrackRepository : IAgendaTrackRepository
{
    private readonly DbSet<AgendaTrack> _agendaTracks;
    private readonly AgendasDbContext _context;

    public AgendaTrackRepository(AgendasDbContext context)
    {
        _context = context;
        _agendaTracks = context.AgendaTracks;
    }

    public Task<AgendaTrack> GetAsync(AggregateId id)
    {
        return _agendaTracks
            .Include(x => x.Slots)
            .SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<AgendaTrack>> BrowsAsync(ConferenceId id)
    {
        return await _agendaTracks
            .AsNoTracking()
            .Include(x => x.Slots)
            .Where(x => x.ConferenceId == id)
            .ToListAsync();
    }

    public Task<bool> ExistsAsync(AggregateId id)
    {
        return _agendaTracks.AnyAsync(x => x.Id == id);
    }

    public async Task AddAsync(AgendaTrack entity)
    {
        _agendaTracks.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(AgendaTrack entity)
    {
        _agendaTracks.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(AgendaTrack entity)
    {
        _agendaTracks.Remove(entity);
        await _context.SaveChangesAsync();
    }
}