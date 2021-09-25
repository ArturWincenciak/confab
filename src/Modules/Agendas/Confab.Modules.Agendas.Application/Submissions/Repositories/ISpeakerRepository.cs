using System.Collections.Generic;
using System.Threading.Tasks;
using Confab.Modules.Agendas.Domain.Submissions.Entities;
using Confab.Shared.Abstractions.Kernel.Types;
using Confab.Shared.Abstractions.Kernel.Types.Base;

namespace Confab.Modules.Agendas.Application.Submissions.Repositories
{
    public interface ISpeakerRepository
    {
        Task<bool> ExistsAsync(AggregateId id);
        Task<IEnumerable<Speaker>> BrowseAsync(IEnumerable<AggregateId> ids);
        Task AddAsync(Speaker entity);
    }
}