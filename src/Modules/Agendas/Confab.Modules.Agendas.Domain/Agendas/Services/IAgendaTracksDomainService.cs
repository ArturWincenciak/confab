using System.Threading.Tasks;
using Confab.Modules.Agendas.Domain.Agendas.Entities;
using Confab.Shared.Kernel.Types.Base;

namespace Confab.Modules.Agendas.Domain.Agendas.Services;

public interface IAgendaTracksDomainService
{
    Task AssignAgendaItemAsync(AgendaTrack agendaTrack, EntityId agendaSlotId, AggregateId agendaItemId);
}