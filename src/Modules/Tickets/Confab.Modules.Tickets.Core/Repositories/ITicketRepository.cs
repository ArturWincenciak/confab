using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Confab.Modules.Tickets.Core.Entities;

namespace Confab.Modules.Tickets.Core.Repositories
{
    internal interface ITicketRepository
    {
        Task<Ticket> GetAsync(Guid conferenceId, Guid userId);
        Task<int> GetCountForConferenceAsync(Guid conferenceId);
        Task<IReadOnlyList<Ticket>> GetForUserIncludingConferenceAsync(Guid userId);
        Task<Ticket> GetAsync(string code);
        Task AddAsync(Ticket entity);
        Task AddManyAsync(IEnumerable<Ticket> entities);
        Task UpdateAsync(Ticket entity);
        Task DeleteAsync(Ticket entity);
    }
}