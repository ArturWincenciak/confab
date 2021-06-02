using System.Threading.Tasks;
using Confab.Modules.Conferences.Core.Entities;

namespace Confab.Modules.Conferences.Core.Policies
{
    internal interface IConferenceDeletionPolice
    {
        Task<bool> CanDeleteAsync(Conference conference);
    }
}