using AspnetCoreMessageIdentity.DAL.Context;
using AspnetCoreMessageIdentity.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspnetCoreMessageIdentity.ViewComponents
{
    public class _MailboxComponentPartial : ViewComponent
    {
        private readonly MailContext _mailContext;
        private readonly UserManager<AppUser> _userManager;

        public _MailboxComponentPartial(UserManager<AppUser> userManager, MailContext mailContext)
        {
            _userManager = userManager;
            _mailContext = mailContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {

            return View();
        }

    }
}
