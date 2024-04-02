using Microsoft.AspNetCore.Mvc;

namespace Confab.Modules.Speakers.Api.Controllers;

[Route(SpeakersModule.BasePath)]
internal class HomeController : SpeakersControllerBase
{
    [HttpGet]
#pragma warning disable CA1822 // Mark members as static
    public ActionResult<string> Get()
#pragma warning restore CA1822 // Mark members as static
    {
        return "Speakers API";
    }
}