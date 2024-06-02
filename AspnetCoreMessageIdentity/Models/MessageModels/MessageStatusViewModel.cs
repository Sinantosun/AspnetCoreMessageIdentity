using AspnetCoreMessageIdentity.DAL.Entities;

namespace AspnetCoreMessageIdentity.Models.MessageModels
{
    public class MessageStatusViewModel
    {
        public int MailsId { get; set; }
        public string Subject { get; set; }
        public bool IsSenderMessageRead { get; set; }

        public int SenderId { get; set; }
        public AppUser Sender { get; set; }

        public int ReceiverId { get; set; }
        public AppUser Receiver { get; set; }

    }
}
