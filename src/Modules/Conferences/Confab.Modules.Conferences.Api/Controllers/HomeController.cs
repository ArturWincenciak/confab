using Microsoft.AspNetCore.Mvc;

namespace Confab.Modules.Conferences.Api.Controllers;

[Route(ConferencesModule.BasePath)]
internal class HomeController : ConferencesControllerBase
{
    [HttpGet]
#pragma warning disable CA1822 // Mark members as static
    public ActionResult<string> Get()
#pragma warning restore CA1822 // Mark members as static
    {
        return "Conferences API";
    }
}