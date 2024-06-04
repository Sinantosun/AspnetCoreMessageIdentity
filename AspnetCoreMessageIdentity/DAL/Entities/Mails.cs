namespace AspnetCoreMessageIdentity.DAL.Entities
{
    public class Mails
    {
        public int MailsId { get; set; }

        public int? MailReplyId { get; set; }

        public string Subject { get; set; }
        public string Content { get; set; }
        public string? Attachment { get; set; }

        public DateTime Date { get; set; }

        public bool IsImportant { get; set; }
        public bool IsRead { get; set; }
        public bool IsTrash { get; set; }
        public bool IsDraft { get; set; }

        public int? MailForwardId { get; set; }

        public DateTime? ForwadDate { get; set; }
        public bool IsForwad { get; set; }

        public DateTime? IsReplyDate { get; set; }
        public bool IsReply { get; set; } 

        public int MailTagsID { get; set; }
        public MailTags MailTag { get; set; }

        public int SenderId { get; set; }
        public AppUser Sender { get; set; }

        public int ReceiverId { get; set; }
        public AppUser Receiver { get; set; }

        public int? OldUserId { get; set; }
        public AppUser OldUser { get; set; } // İLETİLME İŞLEMLERİ İÇİN MESAJIN ESKİ SAHİBİ

    }
}
