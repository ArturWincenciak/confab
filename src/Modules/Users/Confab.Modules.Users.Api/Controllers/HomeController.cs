using Microsoft.AspNetCore.Mvc;

namespace Confab.Modules.Users.Api.Controllers
{
    [Route(UsersModule.BasePath)]
    internal class HomeController : UsersBaseController
    {
        public ActionResult<string> Get() => "Users API";
    }
}