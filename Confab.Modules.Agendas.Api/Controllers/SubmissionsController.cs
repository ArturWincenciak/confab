using System;
using System.Threading.Tasks;
using Confab.Modules.Agendas.Application.Submissions.Commands;
using Confab.Shared.Abstractions.Commands;
using Microsoft.AspNetCore.Mvc;

namespace Confab.Modules.Agendas.Api.Controllers
{
    internal class SubmissionsController : AgendasControllerBase
    {
        private readonly ICommandDispatcher _commandDispatcher;

        public SubmissionsController(ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<object>> GetAsync(Guid id)
        {
            throw new NotImplementedException(nameof(GetAsync));
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync(CreateSubmission command)
        {
            await _commandDispatcher.SendAsync(command);
            return CreatedAtAction("Get", new {command.Id}, null);
        }

        [HttpPut("{id:guid}/approve")]
        public async Task<ActionResult> ApproveAsync(Guid id)
        {
            await _commandDispatcher.SendAsync(new ApproveSubmission(id));
            return NoContent();
        }

        [HttpPut("{id:guid}/reject")]
        public async Task<ActionResult> RejectAsync(Guid id)
        {
            await _commandDispatcher.SendAsync(new RejectSubmission(id));
            return NoContent();
        }
    }
}