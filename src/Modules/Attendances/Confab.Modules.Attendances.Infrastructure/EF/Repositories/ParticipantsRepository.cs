using System.Threading.Tasks;
using Confab.Modules.Attendances.Domain.Entities;
using Confab.Modules.Attendances.Domain.Repositories;
using Confab.Modules.Attendances.Domain.Types;
using Confab.Shared.Kernel.Types;
using Microsoft.EntityFrameworkCore;

namespace Confab.Modules.Attendances.Infrastructure.EF.Repositories;

internal class ParticipantsRepository : IParticipantsRepository
{
    private readonly AttendancesDbContext _context;
    private readonly DbSet<Participant> _participants;

    public ParticipantsRepository(AttendancesDbContext context)
    {
        _context = context;
        _participants = context.Participants;
    }

    public Task<Participant> GetAsync(ParticipantId id)
    {
        return _participants
            .Include(x => x.Attendances)
            .SingleOrDefaultAsync(x => x.Id == id);
    }

    public Task<Participant> GetAsync(ConferenceId conferenceId, UserId userId)
    {
        return _participants
            .Include(x => x.Attendances)
            .SingleOrDefaultAsync(x => x.ConferenceId == conferenceId && x.UserId == userId);
    }

    public async Task AddAsync(Participant participant)
    {
        _participants.Add(participant);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Participant participant)
    {
        _participants.Update(participant);
        await _context.SaveChangesAsync();
    }
}