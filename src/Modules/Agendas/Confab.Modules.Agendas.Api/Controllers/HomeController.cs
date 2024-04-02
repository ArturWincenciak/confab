using Microsoft.AspNetCore.Mvc;

namespace Confab.Modules.Agendas.Api.Controllers;

[Route(AgendasModule.BasePath)]
internal class HomeController : AgendasControllerBase
{
    [HttpGet]
#pragma warning disable CA1822 // Mark members as static
    public ActionResult<string> Get()
#pragma warning restore CA1822 // Mark members as static
    {
        return "Agendas API";
    }
}