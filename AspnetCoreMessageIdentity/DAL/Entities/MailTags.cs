namespace AspnetCoreMessageIdentity.DAL.Entities
{
    public class MailTags
    {
        public int MailTagsID { get; set; }
        public string TagName { get; set; }

        public List<Mails> Mails { get; set; }
    }
}
