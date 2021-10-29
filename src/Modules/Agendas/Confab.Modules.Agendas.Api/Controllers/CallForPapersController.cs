using System;
using System.Threading.Tasks;
using Confab.Modules.Agendas.Application.CallForProps.Commands;
using Confab.Modules.Agendas.Application.CallForProps.Queries;
using Confab.Shared.Abstractions.Commands;
using Confab.Shared.Abstractions.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Confab.Modules.Agendas.Api.Controllers
{
    [Route(AgendasModule.BasePath + "/conferences/{conferenceId:guid}/cfps")]
    [Authorize(Policy = Policy)]
    internal class CallForPapersController : AgendasControllerBase
    {
        public const string Policy = "cfps";

        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;

        public CallForPapersController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<GetCallForPapers.Result>> GetAsync(Guid conferenceId)
        {
            return OkOrNotFound(await _queryDispatcher.QueryAsync(new GetCallForPapers(conferenceId)));
        }

        public record CreateCallForPapersApi(DateTime From, DateTime To);
        [HttpPost]
        public async Task<ActionResult> CreateAsync(Guid conferenceId, CreateCallForPapersApi command)
        {
            await _commandDispatcher.SendAsync(
                new CreateCallForPapers(conferenceId, command.From, command.To));
            return CreatedAtAction("Get", new {conferenceId}, null);
        }

        [HttpPut("openings")]
        public async Task<ActionResult> OpenAsync(Guid conferenceId)
        {
            await _commandDispatcher.SendAsync(new OpenCallForPapers(conferenceId));
            return NoContent();
        }

        [HttpPut("closings")]
        public async Task<ActionResult> CloseAsync(Guid conferenceId)
        {
            await _commandDispatcher.SendAsync(new CloseCallForPapers(conferenceId));
            return NoContent();
        }
    }
}