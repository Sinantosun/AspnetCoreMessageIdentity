using AspnetCoreMessageIdentity.DAL.Context;
using AspnetCoreMessageIdentity.DAL.Entities;
using AspnetCoreMessageIdentity.Models.MessageModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AspnetCoreMessageIdentity.Controllers
{
    public class MailBoxController : Controller
    {
        private readonly MailContext _mailContext;
        private readonly UserManager<AppUser> _userManager;

        public MailBoxController(MailContext mailContext, UserManager<AppUser> userManager)
        {
            _mailContext = mailContext;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var value = await _userManager.FindByNameAsync(User.Identity.Name);
            var MessageList = _mailContext.Mail.OrderBy(x => x.IsRead).Include(t => t.MailTag).Include(x => x.Sender).Include(x => x.Receiver).Include(t => t.OldUser).Where(x => x.ReceiverId == value.Id && x.IsTrash == false && x.IsDraft == false).ToList();
            return View(MessageList);
        }
        [HttpGet]
        public IActionResult Composemessage()
        {
            var MailTagList = _mailContext.MailTags.ToList();
            List<SelectListItem> MailTags = (from x in MailTagList
                                             select new SelectListItem
                                             {
                                                 Text = x.TagName,
                                                 Value = x.MailTagsID.ToString()
                                             }).ToList();
            ViewBag.MailTag = MailTags;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateMessage(CreateMessageViewModel createMessageViewModel)
        {
            createMessageViewModel.IsDraft = false;
            await TCreateMessage(createMessageViewModel);
            return RedirectToAction("Composemessage");

        }
        async Task TCreateMessage(CreateMessageViewModel createMessageViewModel)
        {
            var ReciverUser = await _userManager.FindByEmailAsync(createMessageViewModel.Email);
            if (ReciverUser != null)
            {
                var SenderUser = await _userManager.FindByNameAsync(User.Identity.Name);

                if (createMessageViewModel.formFile != null)
                {
                    var FileName = Guid.NewGuid() + Path.GetExtension(createMessageViewModel.formFile.FileName);
                    var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Attachments/", FileName);
                    var stream = new FileStream(location, FileMode.Create);
                    stream.Dispose();
                    stream.Close();
                    createMessageViewModel.Attachment = "/Attachments/" + FileName;
                }
                _mailContext.Mail.Add(new Mails
                {
                    Content = createMessageViewModel.Content,
                    Date = DateTime.Now,
                    IsDraft = createMessageViewModel.IsDraft,
                    IsImportant = createMessageViewModel.IsImportant,
                    IsForwad = false,
                    IsRead = false,
                    IsReply = false,
                    IsTrash = false,
                    MailTagsID = createMessageViewModel.MailTagsID,
                    Subject = createMessageViewModel.Subject,
                    ReceiverId = ReciverUser.Id,
                    SenderId = SenderUser.Id,

                    Attachment = createMessageViewModel.Attachment,
                });
                _mailContext.SaveChanges();
            };
        }

        [HttpGet]
        public async Task<IActionResult> ReplayMail(int id)
        {
            var value = _mailContext.Mail.Include(t => t.MailTag).FirstOrDefault(x => x.MailsId == id);
            ReplayMailViewModel replayMailViewModel = new ReplayMailViewModel()
            {
                MailsID = value.MailsId,
                Subject = value.Subject + " - Yanıt",
                MailTag = value.MailTag.TagName,


            };
            TempData["ShowButtons"] = "false";
            return View(replayMailViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> ReplayMail(ReplayMailViewModel replayMailViewModel)
        {
            var mailUser = _mailContext.Mail.Include(t => t.Sender).Include(t => t.MailTag).FirstOrDefault(x => x.MailsId == replayMailViewModel.MailsID);
            var SenderUser = await _userManager.FindByNameAsync(User.Identity.Name);
            if (replayMailViewModel.formFile != null)
            {
                var FileName = Guid.NewGuid() + Path.GetExtension(replayMailViewModel.formFile.FileName);
                var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Attachments/", FileName);
                var stream = new FileStream(location, FileMode.Create);
                stream.Dispose();
                stream.Close();
                replayMailViewModel.Attachment = "/Attachments/" + FileName;
            }
            _mailContext.Mail.Add(new Mails
            {
                MailReplyId = replayMailViewModel.MailsID,
                Content = replayMailViewModel.Content,
                Date = DateTime.Now,
                IsDraft = replayMailViewModel.IsDraft,
                IsImportant = replayMailViewModel.IsImportant,
                IsForwad = false,
                IsRead = false,
                IsReply = true,
                IsTrash = false,
                IsReplyDate = DateTime.Now,
                MailTagsID = mailUser.MailTag.MailTagsID,
                Subject = replayMailViewModel.Subject,
                ReceiverId = mailUser.Sender.Id,
                SenderId = SenderUser.Id,

                Attachment = replayMailViewModel.Attachment,
            });
            _mailContext.SaveChanges();
            return RedirectToAction("Index");

        }


        public async Task<IActionResult> UserMails()
        {
            var value = await _userManager.FindByNameAsync(User.Identity.Name);
            var MessageList = _mailContext.Mail.OrderBy(x => x.IsRead).Include(t => t.MailTag).Include(x => x.Sender).Include(x => x.Receiver).Where(x => x.SenderId == value.Id && x.IsTrash == false && x.IsDraft == false).ToList();
            return View(MessageList);
        }
        public async Task<IActionResult> UserImportantMails()
        {
            var value = await _userManager.FindByNameAsync(User.Identity.Name);
            var MessageList = _mailContext.Mail.OrderBy(x => x.IsRead).Include(t => t.MailTag).Include(x => x.Sender).Include(x => x.Receiver).Where(x => x.ReceiverId == value.Id && x.IsTrash == false && x.IsDraft == false && x.IsImportant == true).ToList();
            return View(MessageList);
        }
        public async Task<IActionResult> UserDraftMails()
        {
            var value = await _userManager.FindByNameAsync(User.Identity.Name);
            var MessageList = _mailContext.Mail.OrderBy(x => x.IsRead).Include(t => t.MailTag).Include(x => x.Sender).Include(x => x.Receiver).Where(x => x.ReceiverId == value.Id && x.IsTrash == false && x.IsDraft == true).ToList();
            return View(MessageList);
        }
        public async Task<IActionResult> UserTrashMails()
        {
            var value = await _userManager.FindByNameAsync(User.Identity.Name);
            var MessageList = _mailContext.Mail.OrderBy(x => x.IsRead).Include(t => t.MailTag).Include(x => x.Sender).Include(x => x.Receiver).Where(x => x.ReceiverId == value.Id && x.IsTrash == true || x.SenderId == value.Id && x.IsTrash == true).ToList();
            return View(MessageList);
        }
        [HttpGet]
        public IActionResult ForwardMail(int id)
        {
            var value = _mailContext.Mail.Find(id);
            ForwadMailViewModel forwadMailViewModel = new ForwadMailViewModel()
            {

                MailsId = id,
                Content = value.Content,

            };
            return View(forwadMailViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> ForwardMail(ForwadMailViewModel forwadMailViewModel)
        {

            var mailUser = _mailContext.Mail.Include(t => t.Sender).Include(t => t.MailTag).FirstOrDefault(x => x.MailsId == forwadMailViewModel.MailsId);
            var SenderUser = await _userManager.FindByNameAsync(User.Identity.Name);
            var finByMail = await _userManager.FindByEmailAsync(forwadMailViewModel.Email);
            _mailContext.Mail.Add(new Mails
            {
                Content = mailUser.Content,
                Date = DateTime.Now,
                ForwadDate = DateTime.Now,
                IsDraft = false,
                IsImportant = false,
                IsForwad = true,
                IsRead = false,
                MailForwardId = forwadMailViewModel.MailsId,

                IsReply = false,
                IsTrash = false,
                MailTagsID = mailUser.MailTag.MailTagsID,
                Subject = mailUser.Subject,
                ReceiverId = finByMail.Id,
                SenderId = SenderUser.Id,
                OldUserId = mailUser.SenderId,
                Attachment = mailUser.Attachment,
            });
            _mailContext.SaveChanges();
            return RedirectToAction("Index");
        }



        public IActionResult MoveMessageToTrashFolder(int id)
        {
            var value = _mailContext.Mail.Find(id);
            value.IsTrash = true;
            _mailContext.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
