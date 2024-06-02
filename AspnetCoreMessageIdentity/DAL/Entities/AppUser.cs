using Microsoft.AspNetCore.Identity;

namespace AspnetCoreMessageIdentity.DAL.Entities
{
    public class AppUser : IdentityUser<int>
    {
        public string NameSurname { get; set; }

        public List<Mails> SentMessages { get; set; }
        public List<Mails> ReciverMessages { get; set; }
        public List<ReplyMails> replyMails { get; set; }

        public List<ForwadMails> ForwardReciver { get; set; }
        public List<ForwadMails> ForwardSender { get; set; }
        public List<ForwadMails> ForwadOldUser { get; set; }
    }
}
