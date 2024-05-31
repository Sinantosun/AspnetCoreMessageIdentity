using AspnetCoreMessageIdentity.DAL.Context;
using AspnetCoreMessageIdentity.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AspnetCoreMessageIdentity.ViewComponents
{
    public class _StatisticComponentPartial : ViewComponent
    {
        private readonly MailContext _mailContext;
        private readonly UserManager<AppUser> _userManager;

        public _StatisticComponentPartial(MailContext mailContext, UserManager<AppUser> userManager)
        {
            _mailContext = mailContext;
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            ViewBag.AllMessageCount = _mailContext.Mail.Count();
            ViewBag.IsNotReadCount = _mailContext.Mail.Where(x => x.IsRead == false && x.ReciverNameSurname == user.NameSurname).Count();
            ViewBag.TrashCount = _mailContext.Mail.Where(x => x.IsTrash == true && x.ReciverNameSurname == user.NameSurname).Count();
            ViewBag.DraftCount = _mailContext.Mail.Where(x => x.IsDraft == true && x.ReciverNameSurname == user.NameSurname).Count();


            return View();
        }
    }
}
