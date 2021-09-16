using Microsoft.AspNetCore.Mvc;

namespace Confab.Modules.Users.Api.Controllers
{
    [Route(UsersModule.BasePath)]
    internal class HomeController : UsersBaseController
    {
        [HttpGet]
#pragma warning disable CA1822 // Mark members as static
        public ActionResult<string> Get()
#pragma warning restore CA1822 // Mark members as static
        {
            return "Users API";
        }
    }
}