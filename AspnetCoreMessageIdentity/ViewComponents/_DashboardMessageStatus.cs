using AspnetCoreMessageIdentity.DAL.Context;
using AspnetCoreMessageIdentity.DAL.Entities;
using AspnetCoreMessageIdentity.Models.MessageModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspnetCoreMessageIdentity.ViewComponents
{
    public class _DashboardMessageStatus : ViewComponent
    {
        private readonly MailContext _mailContext;
        private readonly UserManager<AppUser> _userManager;
        public _DashboardMessageStatus(MailContext mailContext, UserManager<AppUser> userManager)
        {
            _mailContext = mailContext;
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var UserMessages = _mailContext.Mail.Include(x => x.Receiver).Include(t => t.Sender).Where(x => x.SenderId == user.Id && x.IsTrash == false).Select(item => new MessageStatusViewModel()
            {
                IsSenderMessageRead = item.IsSenderMessageRead,
                MailsId = item.MailsId,
                ReceiverId = item.ReceiverId,
                SenderId = item.SenderId,
                Subject = item.Subject,
                Receiver=item.Receiver,
                Sender=item.Sender,

            }).Take(5).ToList();

            return View(UserMessages);
        }
    }
}
