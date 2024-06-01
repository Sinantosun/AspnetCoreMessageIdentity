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
            var value = await _userManager.FindByNameAsync(User.Identity.Name);
            var MessageList = _mailContext.Mail.Include(x => x.Sender).Where(x => x.ReceiverId == value.Id).ToList();
            return View(MessageList);
        }
    }
}
