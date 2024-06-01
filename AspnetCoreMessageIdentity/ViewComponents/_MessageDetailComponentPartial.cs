using AspnetCoreMessageIdentity.DAL.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspnetCoreMessageIdentity.ViewComponents
{
    public class _MessageDetailComponentPartial : ViewComponent
    {
        private readonly MailContext _context;

        public _MessageDetailComponentPartial(MailContext mailContext)
        {
            _context = mailContext;
        }

        public IViewComponentResult Invoke(int id)
        {
            var value = _context.Mail.Include(x => x.Sender).FirstOrDefault(x=>x.MailsId == id);
            return View(value);
        }
    }
}
