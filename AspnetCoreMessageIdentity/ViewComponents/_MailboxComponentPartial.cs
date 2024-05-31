using AspnetCoreMessageIdentity.DAL.Context;
using AspnetCoreMessageIdentity.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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
            var value = await _userManager.FindByNameAsync(User.Identity.Name);
            var MessageList = _mailContext.Mail.Where(x => x.ReciverNameSurname == value.NameSurname).ToList();
            return View(MessageList);
        }
    }
}
