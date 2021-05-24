using Microsoft.AspNetCore.Mvc;

namespace Confab.Modules.Conferences.Api.Controllers
{
    [Route("conferences-module")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public ActionResult<string> Get() => "Conferences API";
    }
}
