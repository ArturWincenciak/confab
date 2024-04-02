using System.Collections.Generic;
using System.Threading.Tasks;
using Confab.Modules.Agendas.Domain.Agendas.Entities;
using Confab.Shared.Kernel.Types;
using Confab.Shared.Kernel.Types.Base;

namespace Confab.Modules.Agendas.Domain.Agendas.Repositories;

public interface IAgendaTrackRepository
{
    Task<AgendaTrack> GetAsync(AggregateId id);
    Task<IEnumerable<AgendaTrack>> BrowsAsync(ConferenceId id);
    Task<bool> ExistsAsync(AggregateId id);
    Task AddAsync(AgendaTrack entity);
    Task UpdateAsync(AgendaTrack entity);
    Task DeleteAsync(AgendaTrack entity);
}