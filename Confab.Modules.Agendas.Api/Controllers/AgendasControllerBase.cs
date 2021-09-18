using Confab.Shared.Infrastructure.Api;
using Microsoft.AspNetCore.Mvc;

namespace Confab.Modules.Agendas.Api.Controllers
{
    [ApiController]
    [Route(AgendasModule.BasePath + "/" + "[controller]")]
    [ProducesDefaultContentType]
    internal class AgendasControllerBase : ControllerBase
    {
        protected ActionResult<T> OkOrNotFound<T>(T model, object selector = null)
        {
            return model is { } ? Ok(model) : WhatNotFound<T>(selector);
        }

        private ActionResult<T> WhatNotFound<T>(object what)
        {
            return what is { } ? NotFound(what) : NotFound();
        }
    }
}