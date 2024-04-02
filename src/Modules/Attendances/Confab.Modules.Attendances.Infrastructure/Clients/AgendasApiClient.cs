﻿using System;
using System.Threading.Tasks;
using Confab.Modules.Attendances.Application.Clients.Agendas;
using Confab.Modules.Attendances.Application.Clients.Agendas.DTO;
using Confab.Modules.Attendances.Infrastructure.Clients.Requests;
using Confab.Shared.Abstractions.Modules;

namespace Confab.Modules.Attendances.Infrastructure.Clients;

internal sealed class AgendasApiClient : IAgendasApiClient
{
    private readonly IModuleClient _client;

    public AgendasApiClient(IModuleClient client) =>
        _client = client;

    public Task<RegularAgendaSlotDto> GetRegularAgendaSlotAsync(Guid id) =>
        _client.SendAsync<RegularAgendaSlotDto>(path: "Agendas/GetRegularAgendaSlot",
            request: new GetRegularAgendaSlot(id));

    public Task<AgendaTracksDto> GetAgendaAsync(Guid conferenceId) =>
        _client.SendAsync<AgendaTracksDto>(path: "Agendas/GetAgenda",
            request: new GetAgenda(conferenceId));
}