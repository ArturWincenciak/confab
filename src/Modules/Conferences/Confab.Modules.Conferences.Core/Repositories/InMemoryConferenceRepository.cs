using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Confab.Modules.Conferences.Core.Entities;

namespace Confab.Modules.Conferences.Core.Repositories
{
    internal class InMemoryConferenceRepository : IConferenceRepository
    {
        private readonly List<Conference> _conferences = new();

        public async Task AddAsync(Conference conference)
        {
            _conferences.Add(conference);
            await Task.CompletedTask;
        }

        public Task<Conference> GetAsync(Guid id) => Task.FromResult(_conferences.SingleOrDefault(h => h.Id == id));

        public async Task<IReadOnlyList<Conference>> BrowseAsync()
        {
            await Task.CompletedTask;
            return _conferences;
        }

        public Task UpdateAsync(Conference conference) => Task.CompletedTask;

        public Task DeleteAsync(Conference conference)
        {
            _conferences.Remove(conference);
            return Task.CompletedTask;
        }
    }
}
