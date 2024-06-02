using AspnetCoreMessageIdentity.DAL.Entities;

namespace AspnetCoreMessageIdentity.Models.MessageModels
{
    public class MessageDetailViewModel
    {
        public int MailsId { get; set; }
        public string Subject { get; set; }

        public string Content { get; set; }
        public string? Attachment { get; set; }
        public DateTime Date { get; set; }
        public bool IsImportant { get; set; }
        public bool IsRead { get; set; }

        public int MailTagsID { get; set; }
        public MailTags MailTag { get; set; }

        public int SenderId { get; set; }
        public AppUser Sender { get; set; }

        public int ReceiverId { get; set; }
        public AppUser Receiver { get; set; }

        public string MessageDescription { get; set; }
        public DateTime MessageReplyDate { get; set; }
    }
}
