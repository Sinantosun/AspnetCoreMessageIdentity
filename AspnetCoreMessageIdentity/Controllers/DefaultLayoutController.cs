using Microsoft.AspNetCore.Mvc;

namespace AspnetCoreMessageIdentity.Controllers
{
    public class DefaultLayoutController : Controller
    {
        public IActionResult Index()
        {

            return View();
        }


        public PartialViewResult DefaultHeaderPartial()
        {
            return PartialView();
        }

        public PartialViewResult DefaultSideBarPartial()
        {
            return PartialView();
        }


        public PartialViewResult DefaultScriptsPartial()
        {
            return PartialView();
        }

        public PartialViewResult NotificationPartial()
        {
            return PartialView();
        }
    }
}
