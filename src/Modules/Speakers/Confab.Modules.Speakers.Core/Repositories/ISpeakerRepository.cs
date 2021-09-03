using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Confab.Modules.Speakers.Core.Entities;

namespace Confab.Modules.Speakers.Core.Repositories
{
    internal interface ISpeakerRepository
    {
        Task<bool> ExistsAsync(Guid id);
        Task AddAsync(Speaker speaker);
        Task<Speaker> GetAsync(Guid id);
        Task<IEnumerable<Speaker>> GetAsNoTrackingAsync(string email);
        Task<IReadOnlyList<Speaker>> BrowseAsync();
        Task UpdateAsync(Speaker speaker);
        Task DeleteAsync(Speaker speaker);
    }
}