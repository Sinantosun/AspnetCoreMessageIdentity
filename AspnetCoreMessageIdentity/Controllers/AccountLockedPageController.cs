using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspnetCoreMessageIdentity.Controllers
{
    [AllowAnonymous]
    public class AccountLockedPageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
