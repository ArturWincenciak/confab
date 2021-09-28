using System.Threading.Tasks;
using Confab.Modules.Agendas.Domain.Agendas.Entities;
using Confab.Shared.Abstractions.Kernel.Types.Base;

namespace Confab.Modules.Agendas.Domain.Agendas.Services
{
    internal interface IAgendaTracksDomainService
    {
        Task AssignAgendaItemAsync(AgendaTrack agendaTrack, EntityId agendaSlotId, AggregateId agendaItemId);
    }
}