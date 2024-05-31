using AspnetCoreMessageIdentity.DAL.Entities;
using AspnetCoreMessageIdentity.Models.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AspnetCoreMessageIdentity.Controllers
{
    [AllowAnonymous]
    public class RegisterController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public RegisterController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(RegisterViewModel registerViewModel)
        {
            ModelState.Clear();

            if (ModelState.IsValid)
            {
                if (registerViewModel.IsPolicyRead)
                {
                    AppUser appUser = new AppUser()
                    {
                        NameSurname=registerViewModel.NameSurname,
                        UserName=registerViewModel.UserName,
                        Email=registerViewModel.Mail,
                    };
                    var result = await _userManager.CreateAsync(appUser,registerViewModel.Pwd);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Login");
                    }
                    else
                    {
                        foreach (var item in result.Errors)
                        {
                            ModelState.AddModelError("", item.Description);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("IsPolicyRead", "devam edebilmek için kullanım şartlarını onaylamalısınız");
                }
            }
            return View();
        }
    }
}
