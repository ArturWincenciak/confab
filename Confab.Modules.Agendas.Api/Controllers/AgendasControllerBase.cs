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
            return model is { } ? Ok(model) : WhatNotFound(selector);
        }

        private ActionResult<object> WhatNotFound(object what)
        {
            return what is { } ? NotFound(what) : NotFound();
        }
    }
}