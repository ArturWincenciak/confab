using Microsoft.AspNetCore.Mvc;

namespace Confab.Modules.Conferences.Api.Controllers
{
    [Route(ConferencesModule.BasePath)]
    internal class HomeController : ConferencesControllerBase
    {
        [HttpGet]
        public ActionResult<string> Get()
        {
            return "Conferences API";
        }
    }
}