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
            var value = _context.Mail.Include(x => x.Sender).Include(t=>t.Receiver).Include(z=>z.OldUser).FirstOrDefault(x => x.MailsId == id);


            var value2 = _context.Mail.FirstOrDefault(x => x.SenderId == user.Id && x.MailReplyId==id);
            if (value2 != null)
            {
                ViewBag.test = value2.IsReply;
                ViewBag.test2 = value2.IsReplyDate;
                ViewBag.test3 = value2.MailsId;

                TempData["ShowButtons"] = "true";
            }
            else
            {
                ViewBag.test = false;
            }
       

        
            value.IsRead = true;

            _context.SaveChanges();
            return View(value);
        }
    }
}