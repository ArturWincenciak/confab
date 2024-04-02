using System.Threading.Tasks;
using Confab.Modules.Agendas.Domain.CallForPaper.Entities;
using Confab.Shared.Kernel.Types;

namespace Confab.Modules.Agendas.Application.CallForProps.Repositories;

public interface ICallForPapersRepository
{
    Task<CallForPapers> GetAsync(ConferenceId conferenceId);
    Task<bool> ExistsAsync(ConferenceId conferenceId);
    Task AddAsync(CallForPapers entity);
    Task UpdateAsync(CallForPapers entity);
}