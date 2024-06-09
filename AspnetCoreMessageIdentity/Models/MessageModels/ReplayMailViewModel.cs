﻿namespace AspnetCoreMessageIdentity.Models.MessageModels
{
    public class ReplayMailViewModel
    {
        public int MailsID { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public string MailTag { get; set; }
        public string? Attachment { get; set; }
        public string? AttachmentFileName { get; set; }
        public bool IsImportant { get; set; }
        public bool IsDraft { get; set; }
        public int MailTagsID { get; set; }
        public string Email { get; set; }
        public IFormFile formFile { get; set; }
    }
}
