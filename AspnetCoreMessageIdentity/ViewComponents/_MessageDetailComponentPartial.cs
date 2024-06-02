using AspnetCoreMessageIdentity.DAL.Context;
using AspnetCoreMessageIdentity.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspnetCoreMessageIdentity.ViewComponents
{
    public class _MessageDetailComponentPartial : ViewComponent
    {
        private readonly MailContext _context;
        private readonly UserManager<AppUser> _userManager;
        public _MessageDetailComponentPartial(MailContext mailContext, UserManager<AppUser> userManager)
        {
            _context = mailContext;
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            ViewBag.UserId = user.Id;
            var value = _context.Mail.Include(x => x.Sender).Include(x => x.ReplyMails).Include(x => x.ForwadMails).FirstOrDefault(x => x.MailsId == id);
            ViewBag.Detail = _context.ForwadMails.Include(x => x.SenderUser).Include(t => t.OldUser).Where(x => x.MailsId == id && x.ReciverID == user.Id).FirstOrDefault();
            value.IsRead = true;
            value.IsSenderMessageRead = true;
            _context.SaveChanges();
            return View(value);
        }
    }
}
