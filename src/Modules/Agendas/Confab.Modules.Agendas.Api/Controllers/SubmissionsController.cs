using System;
using System.Threading.Tasks;
using Confab.Modules.Agendas.Application.Submissions.Commands;
using Confab.Modules.Agendas.Application.Submissions.Queries;
using Confab.Shared.Abstractions.Commands;
using Confab.Shared.Abstractions.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Confab.Modules.Agendas.Api.Controllers
{
    internal class SubmissionsController : AgendasControllerBase
    {
        public const string Policy = "submission";

        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;

        public SubmissionsController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
        }

        [Authorize(Policy)]
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<GetSubmission.Result>> GetAsync(Guid id)
        {
            return OkOrNotFound(await _queryDispatcher.QueryAsync(new GetSubmission(id)));
        }

        [Authorize(Policy)]
        [HttpPost]
        public async Task<ActionResult> CreateAsync(CreateSubmission command)
        {
            var createdId = await _commandDispatcher.SendAsync(command);
            var createdSubmission = await _queryDispatcher.QueryAsync(new GetSubmission(createdId.Id));
            return CreatedAtAction("Get", new {createdId.Id}, createdSubmission);
        }

        [Authorize(Policy)]
        [HttpPut("{id:guid}/approvals")]
        public async Task<ActionResult> ApproveAsync(Guid id)
        {
            await _commandDispatcher.SendAsync(new ApproveSubmission(id));
            return NoContent();
        }

        [Authorize(Policy)]
        [HttpPut("{id:guid}/rejections")]
        public async Task<ActionResult> RejectAsync(Guid id)
        {
            await _commandDispatcher.SendAsync(new RejectSubmission(id));
            return NoContent();
        }
    }
}