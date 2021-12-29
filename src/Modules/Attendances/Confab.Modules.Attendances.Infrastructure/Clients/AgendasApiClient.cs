using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Confab.Modules.Attendances.Application.Clients.Agendas;
using Confab.Modules.Attendances.Application.Clients.Agendas.DTO;
using Confab.Modules.Attendances.Infrastructure.Clients.Requests;
using Confab.Shared.Abstractions.Modules;

namespace Confab.Modules.Attendances.Infrastructure.Clients
{
    internal sealed class AgendasApiClient : IAgendasApiClient
    {
        private readonly IModuleClient _client;

        public AgendasApiClient(IModuleClient client)
        {
            _client = client;
        }

        public Task<RegularAgendaSlotDto> GetRegularAgendaSlotAsync(Guid id)
        {
            return _client.SendAsync<RegularAgendaSlotDto>("Agendas/GetRegularAgendaSlot",
                new GetRegularAgendaSlot(id));
        }

        public Task<IEnumerable<AgendaTrackDto>> GetAgendaAsync(Guid conferenceId)
        {
            throw new NotImplementedException();
        }
    }
}