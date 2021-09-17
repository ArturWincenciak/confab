using Confab.Shared.Infrastructure.Api;
using Microsoft.AspNetCore.Mvc;

namespace Confab.Modules.Agendas.Api.Controllers
{
    [ApiController]
    [Route(AgendasModule.BasePath + "/" + "[controller]")]
    [ProducesDefaultContentType]
    internal class AgendasControllerBase : ControllerBase
    {
        protected ActionResult<object> OkOrNotFound(object model, object selector = null)
        {
            if (model is null)
                return WhatNotFound(selector);

            return Ok(model);
        }

        private ActionResult<object> WhatNotFound(object what)
        {
            return what is {} ? NotFound(what) : NotFound();
        }
    }
}