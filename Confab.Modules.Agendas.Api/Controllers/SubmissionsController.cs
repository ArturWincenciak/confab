using System;
using System.Security.Policy;
using System.Threading.Tasks;
using Confab.Modules.Agendas.Application.Submissions.Commands;
using Confab.Modules.Agendas.Application.Submissions.Queries;
using Confab.Shared.Abstractions.Commands;
using Confab.Shared.Abstractions.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Confab.Modules.Agendas.Api.Controllers
{
    internal sealed record TestType(int Age, string Name);

    internal class SubmissionsController : AgendasControllerBase
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;

        public SubmissionsController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
        }


        [HttpGet("test11")]
        public ActionResult<object> GetTest11()
        {
            var result = new TestType(36, "Arthur");
            return NotFound(result);
        }

        [HttpGet("test111")]
        public ActionResult<TestType> GetTest111()
        {
            var result = new TestType(36, "Arthur");
            return NotFound(result);
        }

        [HttpGet("test12")]
        public ActionResult<object> GetTest12()
        {
            return NotFound(new {Id = "this is the id"});
        }

        [HttpGet("test122")]
        public ActionResult<TestType> GetTest122()
        {
            return NotFound(new {Id = "this is the id"});
        }

        [HttpGet("test13")]
        public ActionResult<object> GetTest13()
        {
            return NotFound();
        }

        [HttpGet("test133")]
        public ActionResult<TestType> GetTest133()
        {
            return NotFound();
        }

        [HttpGet("test14")]
        public ActionResult<object> GetTest14()
        {
            var resultDto = new TestType(36, "Arthur");
            var result = TestRetValue1(resultDto);
            return result;
        }

        [HttpGet("test14a")]
        public ActionResult<object> GetTest14a()
        {
            NotFoundResult nf = NotFound();
            ActionResult<TestType> result = nf;
            ActionResult<object> obj = result;
            return obj;
        }

        [HttpGet("test14b")]
        public ActionResult<object> GetTest14b()
        {
            NotFoundResult nf = NotFound();
            ActionResult<object> obj = nf;
            return obj;
        }

        [HttpGet("test144")]
        public ActionResult<TestType> GetTest144()
        {
            var resultDto = new TestType(36, "Arthur");
            var result = TestRetValue1(resultDto);
            return result;
        }

        [HttpGet("test144a")]
        public ActionResult<TestType> GetTest144a()
        {
            NotFoundResult nf = NotFound();
            ActionResult<TestType> result = nf;
            return result;
        }

        private ActionResult<T> TestRetValue1<T>(T resultDto)
        {
            var nf = NotFound();
            ActionResult<T> res = nf;
            return res;
        }


        [HttpGet("test2")]
        public ActionResult<object> GetTest2()
        {
            var resultDto = new TestType(36, "Arthur");
            var result = TestRetValue2(resultDto);
            return result;
        }

        private ActionResult<object> TestRetValue2<T>(T resultDto)
        {
            var nf = NotFound();
            ActionResult<T> res = nf;
            return res;
        }


        [HttpGet("test3")]
        public ActionResult<object> GetTest3()
        {
            var resultDto = new TestType(36, "Arthur");
            var result = TestRetValue3(resultDto);
            return result;
        }

        private ActionResult<object> TestRetValue3<T>(T resultDto)
        {
            var nf = NotFound();
            ActionResult<object> res = nf;
            return res;
        }


        [HttpGet("test4")]
        public ActionResult<object> GetTest4()
        {
            var resultDto = new TestType(36, "Arthur");
            var result = TestRetValue4(resultDto);
            return result;
        }

        private ActionResult<object> TestRetValue4<T>(T resultDto)
        {
            var nf = NotFound();
            ActionResult res = nf;
            return res;
        }


        [HttpGet("{id:guid}")]
        public async Task<ActionResult<object>> GetAsync(Guid id)
        {
            return OkOrNotFound(await _queryDispatcher.QueryAsync(new GetSubmission(id)), new {id});
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync(CreateSubmission command)
        {
            await _commandDispatcher.SendAsync(command); //TODO: popraw tak aby otrzymać ID w odpowiedzi
            //TODO: uczywajac ID strzel zapytaniem aby przekazac w odpowiedzi DTO utworzonego obiektu (value)
            return CreatedAtAction("Get", new {Id = Guid.NewGuid()}, null); //TODO: tutaj ID będzie NULLem
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