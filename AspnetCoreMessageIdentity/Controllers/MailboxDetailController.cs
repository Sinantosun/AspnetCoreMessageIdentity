using AspnetCoreMessageIdentity.DAL.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspnetCoreMessageIdentity.Controllers
{
   
    public class MailboxDetailController : Controller
    {
        private readonly MailContext _mailContext;

        public MailboxDetailController(MailContext mailContext)
        {
            _mailContext = mailContext;
        }

        public IActionResult Index(int id)
        {
            var value = _mailContext.Mail.Include(x => x.Sender).FirstOrDefault(x => x.MailsId == id);
            return View(value);
        }
    }
}
