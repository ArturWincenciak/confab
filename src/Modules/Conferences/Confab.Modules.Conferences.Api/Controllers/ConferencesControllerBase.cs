using Confab.Shared.Infrastructure.Api;
using Microsoft.AspNetCore.Mvc;

namespace Confab.Modules.Conferences.Api.Controllers
{
    [ApiController]
    [Route(ConferencesModule.BasePath + "/" + "[controller]")]
    [ProducesDefaultContentType]
    internal class ConferencesControllerBase : ControllerBase
    {
        protected ActionResult<T> OkOrNotFound<T>(T model)
        {
            if (model is null)
            {
                return NotFound();
            }

            return Ok(model);
        }
    }
}
