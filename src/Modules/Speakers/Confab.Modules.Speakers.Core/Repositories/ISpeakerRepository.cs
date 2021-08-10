using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Confab.Modules.Speakers.Core.Entities;

namespace Confab.Modules.Speakers.Core.Repositories
{
    internal interface ISpeakerRepository
    {
        Task AddAsync(Speaker speaker);
        Task<Speaker> GetAsync(Guid id);
        Task<IReadOnlyList<Speaker>> BrowseAsync();
        Task UpdateAsync(Speaker speaker);
        Task DeleteAsync(Speaker speaker);
    }
}
