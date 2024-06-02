using AspnetCoreMessageIdentity.DAL.Context;
using AspnetCoreMessageIdentity.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspnetCoreMessageIdentity.ViewComponents
{
    public class _DashboardMessagesComponentPartial : ViewComponent
    {
        private readonly MailContext _mailContext;
        private readonly UserManager<AppUser> _userManager;

        public _DashboardMessagesComponentPartial(MailContext mailContext, UserManager<AppUser> userManager)
        {
            _mailContext = mailContext;
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var value = _mailContext.Mail.OrderByDescending(x => x.MailsId).Include(x=>x.Sender).Where(x => x.ReceiverId == user.Id).Take(5).ToList();
            return View(value);
        }
    }
}
