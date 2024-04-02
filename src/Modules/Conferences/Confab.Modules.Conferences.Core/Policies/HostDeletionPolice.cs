using System.Linq;
using System.Threading.Tasks;
using Confab.Modules.Conferences.Core.Entities;

namespace Confab.Modules.Conferences.Core.Policies;

internal class HostDeletionPolice : IHostDeletionPolice
{
    private readonly IConferenceDeletionPolice _conferenceDeletionPolice;

    public HostDeletionPolice(IConferenceDeletionPolice conferenceDeletionPolice) =>
        _conferenceDeletionPolice = conferenceDeletionPolice;

    public async Task<bool> CanDeleteAsync(Host host)
    {
        if (host.Conferences is null || !host.Conferences.Any())
            return true;

        foreach (var conference in host.Conferences)
        {
            if (await _conferenceDeletionPolice.CanDeleteAsync(conference) is false)
                return false;
        }

        return true;
    }
}