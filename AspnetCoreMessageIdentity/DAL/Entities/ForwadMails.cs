namespace AspnetCoreMessageIdentity.DAL.Entities
{
    public class ForwadMails
    {
        public int ForwadMailsID { get; set; }

        public Mails Mails { get; set; }
        public int MailsId { get; set; }

        public AppUser ReciverUser { get; set; }
        public int ReciverID { get; set; }

        public AppUser SenderUser { get; set; }
        public int SenderID { get; set; }

        public AppUser OldUser { get; set; }
        public int OldUserID { get; set; }
    }
}
