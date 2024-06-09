using AspnetCoreMessageIdentity.DAL.Context;
using AspnetCoreMessageIdentity.DAL.Entities;
using AspnetCoreMessageIdentity.Models.MessageModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

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
        public async Task<JsonResult> GetUser()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            return Json(user.NameSurname);

        }
        async Task loadDrdopwn()
        {
            var MailTagList = _mailContext.MailTags.ToList();
            List<SelectListItem> MailTags = (from x in MailTagList
                                             select new SelectListItem
                                             {
                                                 Text = x.TagName,
                                                 Value = x.MailTagsID.ToString()
                                             }).ToList();
            ViewBag.MailTag = MailTags;

            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            TempData["LoginedUser"] = user.NameSurname;
        }

        public async Task<IActionResult> Index()
        {
            var value = await _userManager.FindByNameAsync(User.Identity.Name);
            var MessageList = _mailContext.Mail.OrderBy(x => x.IsRead).Include(t => t.MailTag).Include(x => x.Sender).Include(x => x.Receiver).Include(t => t.OldUser).Where(x => x.ReceiverId == value.Id && x.IsTrash == false && x.IsDraft == false).ToList();
            return View(MessageList);
        }
        [HttpGet]
        public async Task<IActionResult> Composemessage()
        {
            await loadDrdopwn();
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
                    createMessageViewModel.formFile.CopyTo(stream);
                    stream.Close();
                    stream.Dispose();

                    createMessageViewModel.AttachmentFileName = createMessageViewModel.formFile.FileName;
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
                    AttachmentFileName = createMessageViewModel.AttachmentFileName,
                    IsReply = false,
                    IsTrash = false,
                    MailTagsID = createMessageViewModel.MailTagsID,
                    Subject = createMessageViewModel.Subject,
                    ReceiverId = ReciverUser.Id,
                    SenderId = SenderUser.Id,

                    Attachment = createMessageViewModel.Attachment,
                });
                TempData["Result"] = "Mesajınız Gönderildi";
                TempData["icon"] = "success";
                _mailContext.SaveChanges();
            }
            else
            {
                TempData["Result"] = "Email adresi bulunamadı";
                TempData["icon"] = "info";
            };
        }

        [HttpGet]
        public async Task<IActionResult> ReplayMail(int id)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var value = _mailContext.Mail.Include(t => t.MailTag).FirstOrDefault(x => x.MailsId == id);
            ReplayMailViewModel replayMailViewModel = new ReplayMailViewModel()
            {
                MailsID = value.MailsId,
                Subject = value.Subject + " - Yanıt",
                MailTag = value.MailTag.TagName,


            };
            TempData["ShowButtons"] = "false";
            if (value.SenderId != user.Id)
            {
                ViewBag.To = "Kimden";


            }
            else
            {
                ViewBag.To = "Kime";
            }
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
            TempData["Result"] = "Mesaj Yanıtlandı";
            TempData["icon"] = "success";
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
            var MessageList = _mailContext.Mail.OrderBy(x => x.IsRead).Include(t => t.MailTag).Include(x => x.Sender).Include(x => x.Receiver).Where(x => x.SenderId == value.Id && x.IsTrash == false && x.IsDraft == true).ToList();
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
            TempData["Result"] = "Mesaj İletildi";
            TempData["icon"] = "success";
            return RedirectToAction("Index");
        }

        public IActionResult MoveToMailboxFolder(List<int> items)
        {

            for (int i = 0; i < items.Count; i++)
            {
                var value = _mailContext.Mail.FirstOrDefault(x => x.MailsId == items[i]);
                value.IsTrash = false;
                _mailContext.SaveChanges();
            }
            TempData["Result"] = "Seçilen öğeler geri taşındı";
            TempData["icon"] = "success";
            return RedirectToAction("Index");
        }

        public IActionResult MoveMessageToTrashFolder(List<int> items)
        {
            for (int i = 0; i < items.Count; i++)
            {
                var value = _mailContext.Mail.Find(items[i]);
                value.IsTrash = true;
                _mailContext.SaveChanges();
            }

            TempData["Result"] = "Seçilen Öğeler Çöp Kutusuna Taşındı";
            TempData["icon"] = "success";
            return RedirectToAction("Index");
        }
        public IActionResult DeleteMessage(List<int> items)
        {
            for (int i = 0; i < items.Count; i++)
            {
                var value = _mailContext.Mail.Find(items[i]);
                _mailContext.Mail.Remove(value);
                _mailContext.SaveChanges();
            }
            TempData["Result"] = "Seçilen Öğeler Silindi";
            TempData["icon"] = "success";
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> MoveMessageToDraftFolder(CreateMessageViewModel createMessageViewModel)
        {
            createMessageViewModel.IsDraft = true;
            await TCreateMessage(createMessageViewModel);

            TempData["Result"] = "İleti taslak olarak kaydedildi";
            TempData["icon"] = "success";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> EditDraft(int id)
        {
            await loadDrdopwn();

            var value = _mailContext.Mail.Include(x => x.MailTag).Include(t => t.Receiver).FirstOrDefault(x => x.MailsId == id);
            CreateMessageViewModel createMessageViewModel = new CreateMessageViewModel()
            {
                Email = value.Receiver.Email,
                MailTagsID = value.MailTagsID,
                Subject = value.Subject,
                AttachmentFileName = value.AttachmentFileName,
                Attachment = value.Attachment,
                MailsID = id,
                Content = value.Content,

            };
            return View(createMessageViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> EditDraft(CreateMessageViewModel createMessageViewModel)
        {
            var user = await _userManager.FindByEmailAsync(createMessageViewModel.Email);
            var value = _mailContext.Mail.Find(createMessageViewModel.MailsID);
            if (createMessageViewModel.formFile != null)
            {
                var FileName = Guid.NewGuid() + Path.GetExtension(createMessageViewModel.formFile.FileName);
                var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Attachments/", FileName);
                var stream = new FileStream(location, FileMode.Create);
                createMessageViewModel.formFile.CopyTo(stream);
                stream.Close();
                stream.Dispose();

                createMessageViewModel.Attachment = "/Attachments/" + FileName;
                createMessageViewModel.AttachmentFileName = createMessageViewModel.formFile.FileName;
                value.Attachment = createMessageViewModel.Attachment;

            }

            value.IsDraft = false;
            value.Content = createMessageViewModel.Content;
            value.Subject = createMessageViewModel.Subject;
            value.ReceiverId = user.Id;

            value.MailTagsID = createMessageViewModel.MailTagsID;
            value.Date = DateTime.Now;
            value.IsImportant = createMessageViewModel.IsImportant;
            _mailContext.SaveChanges();
            TempData["Result"] = "Mesaj Gönderildi";
            TempData["icon"] = "success";
            return RedirectToAction("Index");
        }





    }
}
