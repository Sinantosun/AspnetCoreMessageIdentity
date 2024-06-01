namespace AspnetCoreMessageIdentity.DAL.Entities
{
    public class ReplyMails
    {
        public int ReplyMailsID { get; set; }
        
        public int MailsId { get; set; }
        public Mails Mails { get; set; }
        public string MessageDescription { get; set; }



    }
}
