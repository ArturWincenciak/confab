using Confab.Shared.Infrastructure.Api;
using Microsoft.AspNetCore.Mvc;

namespace Confab.Modules.Agendas.Api.Controllers
{
    [ApiController]
    [Route(AgendasModule.BasePath + "/" + "[controller]")]
    [ProducesDefaultContentType]
    internal class AgendasControllerBase : ControllerBase
    {
        protected ActionResult<T> OkOrNotFound<T>(T model)
        {
            if (model is null)
                return NotFound();

            return Ok(model);
        }
    }
}