using System;
using Confab.Shared.Abstractions.Queries;
using Confab.Shared.Infrastructure.Api;
using Microsoft.AspNetCore.Mvc;

namespace Confab.Modules.Agendas.Api.Controllers;

[ApiController]
[Route(AgendasModule.BasePath + "/" + "[controller]")]
[ProducesDefaultContentType]
internal class AgendasControllerBase : ControllerBase
{
    protected ActionResult<T> OkOrNotFound<T>(T model, object selector = null) where T : class, IResponseMessage =>
        model is not null ? Ok(model) : WhatNotFound<T>(selector);

    private ActionResult<T> WhatNotFound<T>(object what) =>
        what is not null ? NotFound(what) : NotFound();

    protected void AddResourceIdHeader(Guid id)
    {
        Response.Headers.Add(key: "Resource-ID", value: id.ToString());
    }
}