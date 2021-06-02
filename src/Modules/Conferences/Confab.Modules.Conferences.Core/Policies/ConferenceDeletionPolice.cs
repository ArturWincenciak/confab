using System.Threading.Tasks;
using Confab.Modules.Conferences.Core.Entities;
using Confab.Shared.Abstractions;

namespace Confab.Modules.Conferences.Core.Policies
{
    internal class ConferenceDeletionPolice : IConferenceDeletionPolice
    {
        private readonly IClock _clock;

        public ConferenceDeletionPolice(IClock clock)
        {
            _clock = clock;
        }

        public Task<bool> CanDeleteAsync(Conference conference)
        {
            //TODO: Check if there any participants?
            var canDelete = _clock.CurrentDate().Date.AddDays(7) < conference.From.Date;
            return Task.FromResult(canDelete);
        }
    }
}