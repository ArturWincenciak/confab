using Microsoft.AspNetCore.Mvc;

namespace Confab.Modules.Speakers.Api.Controllers
{
    [Route(SpeakersModule.BasePath)]
    internal class HomeController : SpeakersControllerBase
    {
        [HttpGet]
        public ActionResult<string> Get()
        {
            return "Speakers API";
        }
    }
}