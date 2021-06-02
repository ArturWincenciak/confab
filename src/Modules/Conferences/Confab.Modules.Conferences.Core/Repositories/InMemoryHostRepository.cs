using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Confab.Modules.Conferences.Core.Entities;

namespace Confab.Modules.Conferences.Core.Repositories
{
    internal class InMemoryHostRepository : IHostRepository
    {
        private readonly List<Host> _hosts = new List<Host>();

        public async Task AddAsync(Host host)
        {
            _hosts.Add(host);
            await Task.CompletedTask;
        }

        public Task<Host> GetAsync(Guid id) => Task.FromResult(_hosts.SingleOrDefault(h => h.Id == id));

        public async Task<IEnumerable<Host>> BrowseAsync()
        {
            await Task.CompletedTask;
            return _hosts;
        }

        public Task UpdateAsync(Host host) => Task.CompletedTask;

        public Task DeleteAsync(Host host)
        {
            _hosts.Remove(host);
            return Task.CompletedTask;
        }
    }
}