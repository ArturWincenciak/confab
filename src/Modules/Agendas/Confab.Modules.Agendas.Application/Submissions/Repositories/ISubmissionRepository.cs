using System.Threading.Tasks;
using Confab.Modules.Agendas.Domain.Submissions.Entities;
using Confab.Shared.Abstractions.Kernel.Types;
using Confab.Shared.Abstractions.Kernel.Types.Base;

namespace Confab.Modules.Agendas.Application.Submissions.Repositories
{
    public interface ISubmissionRepository
    {
        Task<Submission> GetAsync(AggregateId id);
        Task AddAsync(Submission entity);
        Task UpdateAsync(Submission entity);
    }
}