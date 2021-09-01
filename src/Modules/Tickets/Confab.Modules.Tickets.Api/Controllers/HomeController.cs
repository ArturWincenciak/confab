using Microsoft.AspNetCore.Mvc;

namespace Confab.Modules.Tickets.Api.Controllers
{
    [Route(TicketsModule.BasePath)]
    internal class HomeController : TicketsControllerBase
    {
        [HttpGet]
        public ActionResult<string> Get()
        {
            return "Tickets API";
        }
    }
}