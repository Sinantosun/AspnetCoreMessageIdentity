using Microsoft.AspNetCore.Mvc;

namespace AspnetCoreMessageIdentity.Controllers
{
    
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult DümendenBirView()
        {
            return View();
        }
    }
}
