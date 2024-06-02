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
            ViewBag.FindReplay = _context.replyMails.Include(x => x.AppUserReciver).Include(t => t.Mails).Where(x => x.MailsId == id && x.AppUserId == user.Id).FirstOrDefault();
            ViewBag.FindForwad = _context.ForwadMails.Include(x => x.Mails).Include(x => x.SenderUser).Where(x => x.MailsId == id && x.ReciverID == user.Id).FirstOrDefault();
            return View(value);
        }
    }
}
