using AspnetCoreMessageIdentity.DAL.Context;
using AspnetCoreMessageIdentity.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspnetCoreMessageIdentity.Controllers
{
    public class UserMailsController : Controller
    {
        private readonly MailContext _mailContext;
        private readonly UserManager<AppUser> _userManager;

        public UserMailsController(MailContext mailContext, UserManager<AppUser> userManager)
        {
            _mailContext = mailContext;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var value = await _userManager.FindByNameAsync(User.Identity.Name);
            ViewBag.UserId1 = value.Id;
            var MessageList = _mailContext.Mail.OrderBy(x => x.IsRead).Include(t => t.MailTag).Include(x => x.ForwadMails).Include(x => x.Sender).Include(x => x.Receiver).Where(x => x.SenderId == value.Id && x.IsTrash == false && x.IsDraft == false).ToList();
            TempData["test"] = "test";
            return View(MessageList);
        }
    }
}
