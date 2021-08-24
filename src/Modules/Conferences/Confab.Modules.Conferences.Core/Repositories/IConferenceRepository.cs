using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Confab.Modules.Conferences.Core.Entities;

namespace Confab.Modules.Conferences.Core.Repositories
{
    internal interface IConferenceRepository
    {
        Task AddAsync(Conference conference);
        Task<Conference> GetAsync(Guid id);
        Task<IReadOnlyList<Conference>> BrowseAsync();
        Task UpdateAsync(Conference conference);
        Task DeleteAsync(Conference conference);
    }
}