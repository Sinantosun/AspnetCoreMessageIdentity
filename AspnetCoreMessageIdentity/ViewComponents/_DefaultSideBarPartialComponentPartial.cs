﻿using AspnetCoreMessageIdentity.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AspnetCoreMessageIdentity.ViewComponents
{
    public class _DefaultSideBarPartialComponentPartial : ViewComponent
    {
        private readonly UserManager<AppUser> _userManager;

        public _DefaultSideBarPartialComponentPartial(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            ViewBag.UserNameSurname = user.NameSurname;
            ViewBag.UserUserName = user.UserName;
            return View();
        }
    }
}
