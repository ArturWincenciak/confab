using Microsoft.AspNetCore.Mvc;

namespace Confab.Modules.Conferences.Api.Controllers
{
    [ApiController]
    [Route(BasePath + "[controller]")]
    internal class ConferencesControllerBase : ControllerBase
    {
        protected const string BasePath = "conferences-module";

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