namespace AspnetCoreMessageIdentity.DAL.Entities
{
    public class Mails
    {
        public int MailsId { get; set; }
        public string Subject { get; set; }
        public string ReciverNameSurname { get; set; }
        public string SenderNameSurname { get; set; }
        public string Content { get; set; }
        public string? Attachment { get; set; }
        public DateTime Date { get; set; }
        public bool IsImportant { get; set; }
        public bool IsRead { get; set; }
        public bool IsTrash { get; set; }
        public bool IsDraft { get; set; }
        public bool IsSenderMessageRead { get; set; }

        public int MailTagsID { get; set; }
        public MailTags MailTag { get; set; }
    }
}
