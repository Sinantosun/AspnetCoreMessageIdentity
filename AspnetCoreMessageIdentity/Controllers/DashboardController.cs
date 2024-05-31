using Microsoft.AspNetCore.Mvc;

namespace AspnetCoreMessageIdentity.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
