namespace AspnetCoreMessageIdentity.DAL.Entities
{
    public class Mails
    {
        public int MailsId { get; set; }
        public string Subject { get; set; }

        public string Content { get; set; }
        public string? Attachment { get; set; }
        public DateTime Date { get; set; }
        public bool IsImportant { get; set; }
        public bool IsRead { get; set; }
        public bool IsTrash { get; set; }
        public bool IsDraft { get; set; }
        public bool IsSenderMessageRead { get; set; }
        public bool IsForwad { get; set; } //iletilme durumunda true olur


        public int MailTagsID { get; set; }
        public MailTags MailTag { get; set; }

        public int SenderId { get; set; }
        public AppUser Sender { get; set; }

        public int ReceiverId { get; set; }
        public AppUser Receiver { get; set; }

        public List<ForwadMails> ForwadMails { get; set; }


        public List<ReplyMails> ReplyMails { get; set; }

    }
}
