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
            ViewBag.UserId1 = value.Id;
            var MessageList = _mailContext.Mail.OrderBy(x => x.IsRead).Include(t => t.MailTag).Include(x => x.ForwadMails).Include(x => x.Sender).Include(x => x.Receiver).Where(x => x.ReceiverId == value.Id && x.IsTrash == false && x.IsDraft == false).ToList();
            return View(MessageList);
        }

    }
}
