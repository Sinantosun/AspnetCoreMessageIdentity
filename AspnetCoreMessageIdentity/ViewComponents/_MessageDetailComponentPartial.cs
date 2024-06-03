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

        public async Task<IViewComponentResult> InvokeAsync(int id, string? ut)
        {
            if (ut != null)
            {
                TempData["ShowButtons"] = "true";
            }

            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var value = _context.Mail.Include(x => x.Sender).FirstOrDefault(x => x.MailsId == id);
            value.IsRead = true;
            _context.SaveChanges();
            return View(value);
        }
    }
}
