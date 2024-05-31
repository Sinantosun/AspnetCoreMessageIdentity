using AspnetCoreMessageIdentity.DAL.Context;
using Microsoft.AspNetCore.Mvc;

namespace AspnetCoreMessageIdentity.ViewComponents
{
    public class _NavbarComponentPartial : ViewComponent
    {
        private readonly MailContext _mailContext;

        public _NavbarComponentPartial(MailContext mailContext)
        {
            _mailContext = mailContext;
        }

        public IViewComponentResult Invoke()
        {
            var value = _mailContext.Mail.ToList();
            return View(value);
        }
    }
}
