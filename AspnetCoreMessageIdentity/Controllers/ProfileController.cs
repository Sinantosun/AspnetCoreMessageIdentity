using AspnetCoreMessageIdentity.DAL.Entities;
using AspnetCoreMessageIdentity.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AspnetCoreMessageIdentity.Controllers
{
    public class ProfileController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public ProfileController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            UserResultViewModel userResultViewModel = new UserResultViewModel()
            {
                NameSurname = user.NameSurname,
                UserName = user.UserName,
            };
            return View(userResultViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Index(UserResultViewModel model)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var checkPassword = await _userManager.CheckPasswordAsync(user, model.OldPwd);
            if (checkPassword)
            {
                if (model.Pwd == model.ConfrimPwd)
                {
                    user.UserName = model.UserName;
                    user.NameSurname = model.NameSurname;
                    if (model.Pwd != null)
                    {
                        var result = await _userManager.ChangePasswordAsync(user, model.OldPwd, model.Pwd);
                        if (!result.Succeeded)
                        {
                            foreach (var item in result.Errors)
                            {
                                ModelState.AddModelError("", item.Description);
                            }
                            return View();
                        }
                    }
                    await _userManager.UpdateAsync(user);
                    return RedirectToAction("Index", "Login");
                }
                else
                {
                    ModelState.AddModelError("", "Girilen şifreler uyuşmuyor");
                }
            }
            else
            {
                ModelState.AddModelError("", "Eski Şifreniz hatalı");
            }


            return View();

        }
    }
}
