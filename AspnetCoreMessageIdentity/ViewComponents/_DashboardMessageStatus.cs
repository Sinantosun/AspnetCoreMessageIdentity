using AspnetCoreMessageIdentity.DAL.Context;
using AspnetCoreMessageIdentity.DAL.Entities;
using AspnetCoreMessageIdentity.Models.MessageModels;
using Humanizer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AspnetCoreMessageIdentity.ViewComponents
{
    public class _DashboardMessageStatus : ViewComponent
    {
        private readonly MailContext _mailContext;
        private readonly UserManager<AppUser> _userManager;
        public _DashboardMessageStatus(MailContext mailContext, UserManager<AppUser> userManager)
        {
            _mailContext = mailContext;
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var mails = _mailContext.Mail.Include(t => t.Receiver).Where(x => x.SenderId == user.Id).OrderByDescending(x=>x.MailsId).Take(5).ToList();


            foreach (var mail in mails)
            {
                if (mail.IsReciverReadMessage)
                {
                    DateTime dateNow = Convert.ToDateTime(DateTime.Now.ToString("g"));
                    DateTime MessageReplayDate = Convert.ToDateTime(mail.ReciverReadMessageDate);
                    TimeSpan times = dateNow - MessageReplayDate;

                    if (times.TotalMinutes < 60)
                    {
                        if (times.TotalMinutes == 0)
                        {
                            ViewBag.Time = "Az Önce";
                        }
                        else
                        {
                            ViewBag.Time = $"{(int)times.TotalMinutes} dakika önce";

                        }
                    }
                    else if (times.TotalHours < 24)
                    {
                        ViewBag.Time = $"{(int)times.TotalHours} saat önce";
                    }
                    else if (times.TotalDays < 30)
                    {
                        ViewBag.Time = $"{(int)times.TotalDays} gün önce";
                    }
                    else
                    {
                        ViewBag.Time = MessageReplayDate.ToString("MM") + MessageReplayDate.ToString("MMMM") + " günü";
                    }

                }
            }

            return View(mails);
        }
    }
}
