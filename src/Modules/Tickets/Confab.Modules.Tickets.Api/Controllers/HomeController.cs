using Microsoft.AspNetCore.Mvc;

namespace Confab.Modules.Tickets.Api.Controllers;

[Route(TicketsModule.BasePath)]
internal class HomeController : TicketsControllerBase
{
    [HttpGet]
#pragma warning disable CA1822 // Mark members as static
    public ActionResult<string> Get()
#pragma warning restore CA1822 // Mark members as static
    {
        return "Tickets API";
    }
}