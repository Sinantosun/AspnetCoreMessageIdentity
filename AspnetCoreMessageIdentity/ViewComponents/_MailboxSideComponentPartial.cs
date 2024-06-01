using AspnetCoreMessageIdentity.DAL.Context;
using AspnetCoreMessageIdentity.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AspnetCoreMessageIdentity.ViewComponents
{
    public class _MailboxSideComponentPartial : ViewComponent
    {
        private readonly MailContext _context;
        private readonly UserManager<AppUser> _userManager;
        public _MailboxSideComponentPartial(MailContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            ViewBag.MessageCount = _context.Mail.Where(x => x.ReceiverId == user.Id).Count();
            ViewBag.SentMessageCount = _context.Mail.Where(x => x.SenderId == user.Id).Count();
            ViewBag.ImportantMessageCount = _context.Mail.Where(x => x.ReceiverId == user.Id && x.IsImportant == true).Count();
            ViewBag.DraftCount = _context.Mail.Where(x => x.ReceiverId == user.Id && x.IsDraft == true).Count();
            ViewBag.TrashCount = _context.Mail.Where(x => x.ReceiverId == user.Id && x.IsTrash == true).Count();


            return View();
        }
    }
}
