using AspnetCoreMessageIdentity.DAL.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspnetCoreMessageIdentity.ViewComponents
{
    public class _ReplyMessageComponentPartial : ViewComponent
    {
        private readonly MailContext _mailContext;

        public _ReplyMessageComponentPartial(MailContext mailContext)
        {
            _mailContext = mailContext;
        }

        public IViewComponentResult Invoke(int id)
        {
            var value = _mailContext.replyMails.Include(x=>x.Mails).Where(x=>x.MailsId== id).FirstOrDefault();  
            return View(value);
        }
    }
}
