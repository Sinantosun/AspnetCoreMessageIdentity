using AspnetCoreMessageIdentity.DAL.Entities;

namespace AspnetCoreMessageIdentity.Models.MessageModels
{
    public class ReplayMailViewModel
    {

        public string Subject { get; set; }
        public string Content { get; set; }
        public string MailTagName { get; set; }
        public string? Attachment { get; set; }
        public string? AttachmentFileName { get; set; }
        public bool IsImportant { get; set; }
        public bool IsDraft { get; set; }
        public int MailTagsID { get; set; }
        public string Email { get; set; }
        public IFormFile formFile { get; set; }

        public int ReplayMailViewModelId { get; set; }

        public MailTags MailTag { get; set; }

    }
}
