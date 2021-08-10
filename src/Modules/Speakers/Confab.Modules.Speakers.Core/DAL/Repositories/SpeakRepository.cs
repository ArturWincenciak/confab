using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Confab.Modules.Speakers.Core.Entities;
using Confab.Modules.Speakers.Core.Repositories;

namespace Confab.Modules.Speakers.Core.DAL.Repositories
{
    internal class SpeakRepository : ISpeakerRepository
    {
        public Task AddAsync(Speaker speaker)
        {
            throw new NotImplementedException();
        }

        public Task<Speaker> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Speaker>> BrowseAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Speaker speaker)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Speaker speaker)
        {
            throw new NotImplementedException();
        }
    }
}
