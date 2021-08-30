using System;
using System.Threading.Tasks;
using Confab.Modules.Tickets.Core.Entities;

namespace Confab.Modules.Tickets.Core.Repositories
{
    internal interface IConferenceRepository
    {
        Task<Conference> GetAsync(Guid id);
        Task AddAsync(Conference entity);
        Task UpdateAsync(Conference entity);
        Task DeleteAsync(Conference entity);
    }
}