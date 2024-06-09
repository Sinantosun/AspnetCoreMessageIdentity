using AspnetCoreMessageIdentity.DAL.Entities;
using AspnetCoreMessageIdentity.Data;
using AspnetCoreMessageIdentity.Models.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AspnetCoreMessageIdentity.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;

        public LoginController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }
        [HttpGet]
        public IActionResult Index(string? returnUrl)
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel model, string? returnUrl)
        {
            await _signInManager.SignOutAsync();
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Pwd, false, true);
                if (result.Succeeded)
                {
                    var user = await _userManager.FindByNameAsync(model.UserName);
                    if (string.IsNullOrEmpty(returnUrl))
                    {
                        TempData["UserStatus"] = "Online";
                        TempData["UserNameSurname"] = user.NameSurname;
                        return RedirectToAction("Index", "Dashboard");
                    }
                    else
                    {
                        TempData["UserNameSurname"] = user.NameSurname;
                        TempData["UserStatus"] = "Online";
                        return Redirect(returnUrl);
                    }

                }
                else if (result.IsLockedOut)
                {
                    return RedirectToAction("Index", "AccountLockedPage");
                }
                else
                {
                    ModelState.AddModelError("", "Kullanıcı adı veya şifre hatalı");
                }
            }

            return View();
        }
        public async Task<IActionResult> SignOut()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var client = ClientSources.userClients.FirstOrDefault(x => x.NameSurname == user.NameSurname);
            if (client != null)
            {
                ClientSources.userClients.Remove(client);
            }

            await _signInManager.SignOutAsync();
            return RedirectToAction("Index");
        }
    }
}
