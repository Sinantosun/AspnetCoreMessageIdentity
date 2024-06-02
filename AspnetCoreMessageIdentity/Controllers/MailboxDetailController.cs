using AspnetCoreMessageIdentity.DAL.Context;
using AspnetCoreMessageIdentity.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspnetCoreMessageIdentity.Controllers
{

    public class MailboxDetailController : Controller
    {
        private readonly MailContext _context;
        private readonly UserManager<AppUser> _userManager;
        public MailboxDetailController(MailContext mailContext, UserManager<AppUser> userManager)
        {
            _context = mailContext;
            _userManager = userManager;
        }


        public async Task<IActionResult> Index(int id, string? ut) // ut => use tempdata
        {
            if (ut != null)
            {
                TempData["ShowButtons"] = "true";
            }

            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            ViewBag.UserId = user.Id;
            var value = _context.Mail.Include(x => x.Sender).Include(x => x.ReplyMails).Include(x => x.ForwadMails).FirstOrDefault(x => x.MailsId == id);
            ViewBag.Detail = _context.ForwadMails.Include(x => x.SenderUser).Include(t => t.OldUser).Where(x => x.MailsId == id && x.ReciverID == user.Id).FirstOrDefault();
            value.IsRead = true;
            value.IsSenderMessageRead = true;
            //yanıtlayan kişi yanıtlanacak kişi
            _context.SaveChanges();
            return View(value);
        }
    }
}
