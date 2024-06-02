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
        public IActionResult Index()
        {

            return View();
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
                    IsRead = false,
                    IsSenderMessageRead = false,
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

        [HttpPost]
        public async Task<IActionResult> CreateMessage(CreateMessageViewModel createMessageViewModel)
        {
            createMessageViewModel.IsDraft = false;
            await TCreateMessage(createMessageViewModel);
            return RedirectToAction("Composemessage");

        }


        [HttpPost]
        public async Task<IActionResult> MoveMessageToDraftFolder(CreateMessageViewModel createMessageViewModel)
        {
            createMessageViewModel.IsDraft = true;
            await TCreateMessage(createMessageViewModel);
            return RedirectToAction("Composemessage");
        }


        [HttpGet]
        public async Task<IActionResult> ReplayMail(int id)
        {
            var value = _mailContext.Mail.Include(x => x.Receiver).FirstOrDefault(x => x.MailsId == id);

            ReplayMailViewModel replayMailViewModel = new ReplayMailViewModel()
            {
                Email = value.Receiver.Email,
                MailsID = value.MailsId,
            };
            TempData["ShowButtons"] = "false";
            return View(replayMailViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> ReplayMail(ReplayMailViewModel replayMailViewModel)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            _mailContext.replyMails.Add(new ReplyMails
            {
                MailsId = replayMailViewModel.MailsID,
                MessageDescription = replayMailViewModel.Description,
                MessageReplyDate = DateTime.Now,
                AppUserId = user.Id,//MESAJI YANITLAYAN KİŞİNİN İD DEĞERİ

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

        [HttpGet]
        public IActionResult ForwardMail(int id)
        {
            var message = _mailContext.Mail.Find(id);
            ForwadMailViewModel forwadMailViewModel = new ForwadMailViewModel()
            {
                Content = message.Content,
                MailsId = message.MailsId,
            };

            return View(forwadMailViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> ForwardMail(ForwadMailViewModel model)
        {
            var message = _mailContext.Mail.Find(model.MailsId);
            var Reciveruser = await _userManager.FindByEmailAsync(model.Email);
            var SenderUser = await _userManager.FindByNameAsync(User.Identity.Name);
            _mailContext.ForwadMails.Add(new ForwadMails
            {
                MailsId = model.MailsId,
                ReciverID = Reciveruser.Id,
                SenderID = SenderUser.Id,
            });


            _mailContext.Mail.Add(new Mails
            {
                Attachment = message.Attachment,
                IsRead = message.IsRead,
                IsImportant = message.IsImportant,
                Content = message.Content,
                IsDraft = message.IsDraft,
                Date = message.Date,
                IsSenderMessageRead = message.IsSenderMessageRead,
                IsTrash = message.IsTrash,
                Subject = message.Subject,
                ReceiverId=Reciveruser.Id,
                SenderId=SenderUser.Id,
                MailTagsID = message.MailTagsID,
            });


            _mailContext.SaveChanges();
            return RedirectToAction("Index");
        }



    }
}
