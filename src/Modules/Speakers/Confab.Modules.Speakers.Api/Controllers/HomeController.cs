using Microsoft.AspNetCore.Mvc;

namespace Confab.Modules.Speakers.Api.Controllers
{
    [Route(SpeakersModule.BasePath)]
    internal class HomeController : SpeakerControllerBase
    {
        [HttpGet]
        public ActionResult<string> Get() => "Speakers API";
    }
}
