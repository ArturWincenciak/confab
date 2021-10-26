using System.Threading.Tasks;
using Confab.Modules.Agendas.Domain.CallForPaper.Entities;
using Confab.Shared.Abstractions.Kernel.Types;

namespace Confab.Modules.Agendas.Application.Submissions.Repositories
{
    public interface ICallForPapersRepository
    {
        Task<CallForPapers> GetAsync(ConferenceId conferenceId);
        Task<bool> ExistsAsync(ConferenceId conferenceId);
        Task AddAsync(CallForPapers entity);
        Task UpdateAsync(CallForPapers entity);
    }
}