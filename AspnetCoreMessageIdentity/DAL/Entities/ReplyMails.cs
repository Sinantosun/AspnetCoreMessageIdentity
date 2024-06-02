using System.ComponentModel.DataAnnotations;

namespace AspnetCoreMessageIdentity.DAL.Entities
{
    public class ReplyMails
    {
        [Key]

        public int ReplyMailsID { get; set; }

        public int MailsId { get; set; }
        public Mails Mails { get; set; }
        public string MessageDescription { get; set; }
        public DateTime MessageReplyDate { get; set; }

        public AppUser AppUserReciver { get; set; }
        public int AppUserId { get; set; }


    }
}
