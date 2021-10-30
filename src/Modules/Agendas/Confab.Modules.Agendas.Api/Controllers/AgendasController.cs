﻿using System;
using System.Threading.Tasks;
using Confab.Modules.Agendas.Application.Agendas.Commands;
using Confab.Modules.Agendas.Application.Agendas.Queries;
using Confab.Shared.Abstractions.Commands;
using Confab.Shared.Abstractions.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Confab.Modules.Agendas.Api.Controllers
{
    [Route(AgendasModule.BasePath + "/agendas/{conferenceId:guid}")]
    [Authorize(Policy = Policy)]
    internal class AgendasController : AgendasControllerBase
    {
        internal const string Policy = "agendas";
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;

        public AgendasController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<GetAgendaTrack.Result>> GetAgendaTrackAsync(Guid conferenceId)
        {
            return OkOrNotFound(await _queryDispatcher.QueryAsync(new GetAgendaTrack(conferenceId)));
        }

        [HttpGet("items")]
        [AllowAnonymous]
        public async Task<ActionResult<GetAgenda.Result>> GetAgendaAsync(Guid conferenceId)
        {
            return OkOrNotFound(await _queryDispatcher.QueryAsync(new GetAgenda(conferenceId)));
        }

        [HttpGet("items/{id:guid}")]
        [AllowAnonymous]
        public async Task<ActionResult<BrowsAgendaItems.Result>> BrowseAgendaItemsAsync(Guid conferenceId)
        {
            return OkOrNotFound(await _queryDispatcher.QueryAsync(new BrowsAgendaItems(conferenceId)));
        }

        [HttpPost("tracks")]
        public async Task<ActionResult> CreateAgendaTrackAsync(Guid conferenceId, CreateAgendaTrackApi command)
        {
            var created = await _commandDispatcher.SendAsync(new CreateAgendaTrack(conferenceId, command.Name));
            var agendaTrack = await _queryDispatcher.QueryAsync(new GetAgendaTrack(conferenceId));
            AddResourceIdHeader(created.Id);
            return CreatedAtAction("GetAgendaTrack", new {created.Id}, agendaTrack);
        }

        [HttpPost("slots")]
        public async Task<ActionResult> CreateAgendaSlotAsync(CreateAgendaSlot command)
        {
            var created = await _commandDispatcher.SendAsync(command);
            AddResourceIdHeader(created.Id);
            return NoContent();
        }

        [HttpPost("slots/placeholder-assignments")]
        public async Task<ActionResult> AssignPlaceholderAgendaSlotAsync(AssignPlaceholderAgendaSlot command)
        {
            await _commandDispatcher.SendAsync(command);
            return NoContent();
        }

        [HttpPost("slots/regular-assignments")]
        public async Task<ActionResult> AssignRegularAgendaSlotAsync(AssignRegularAgendaSlot command)
        {
            await _commandDispatcher.SendAsync(command);
            return NoContent();
        }

        public record CreateAgendaTrackApi(string Name);
    }
}