using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Confab.Modules.Speakers.Core.DTO;

namespace Confab.Modules.Speakers.Core.Services
{
    internal class SpeakerService : ISpeakerService
    {
        public Task AddAsync(SpeakerDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<SpeakerDto> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<SpeakerDto>> BrowseAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(SpeakerDto dto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
