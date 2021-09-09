using Microsoft.AspNetCore.Mvc;

namespace Confab.Modules.Agendas.Api.Controllers
{
    [Route(AgendasModule.BasePath)]
    internal class HomeController : AgendasControllerBase
    {
        [HttpGet]
        public ActionResult<string> Get()
        {
            return "Agendas API";
        }
    }
}