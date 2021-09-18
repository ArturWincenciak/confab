using System;
using System.Threading.Tasks;
using Confab.Modules.Agendas.Application.Submissions.Commands;
using Confab.Modules.Agendas.Application.Submissions.Queries;
using Confab.Shared.Abstractions.Commands;
using Confab.Shared.Abstractions.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Confab.Modules.Agendas.Api.Controllers
{
    internal class SubmissionsController : AgendasControllerBase
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;

        public SubmissionsController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<GetSubmission.SubmissionDto>> GetAsync(Guid id)
        {
            return OkOrNotFound(await _queryDispatcher.QueryAsync(new GetSubmission(id)));
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync(CreateSubmission command)
        {
            var id = await _commandDispatcher.SendAsync(command);

            //TODO: uczywajac ID strzel zapytaniem aby przekazac w odpowiedzi DTO utworzonego obiektu (value)

            return CreatedAtAction("Get", new {Id = id }, null);
        }

        [HttpPut("{id:guid}/approvals")]
        public async Task<ActionResult> ApproveAsync(Guid id)
        {
            await _commandDispatcher.SendAsync(new ApproveSubmission(id));
            return NoContent();
        }

        [HttpPut("{id:guid}/rejections")]
        public async Task<ActionResult> RejectAsync(Guid id)
        {
            await _commandDispatcher.SendAsync(new RejectSubmission(id));
            return NoContent();
        }
    }
}