using AspnetCoreMessageIdentity.DAL.Context;
using Microsoft.AspNetCore.Mvc;

namespace AspnetCoreMessageIdentity.ViewComponents
{
    public class _DashboardMessageStatus : ViewComponent
    {
        private readonly MailContext _mailContext;

        public _DashboardMessageStatus(MailContext mailContext)
        {
            _mailContext = mailContext;
        }

        public IViewComponentResult Invoke()
        {
            var value = _mailContext.Mail.ToList();
            return View();
        }
    }
}
