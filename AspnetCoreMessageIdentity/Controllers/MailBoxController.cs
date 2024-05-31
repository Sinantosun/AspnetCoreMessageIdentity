using AspnetCoreMessageIdentity.DAL.Context;
using AspnetCoreMessageIdentity.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AspnetCoreMessageIdentity.Controllers
{
    public class MailBoxController : Controller
    {
       


        public async Task<IActionResult> Index()
        {
        
            return View();
        }
    }
}
