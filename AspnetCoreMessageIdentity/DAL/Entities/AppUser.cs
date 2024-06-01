using Microsoft.AspNetCore.Identity;

namespace AspnetCoreMessageIdentity.DAL.Entities
{
    public class AppUser : IdentityUser<int>
    {
        public string NameSurname { get; set; }

        public List<Mails> SentMessages { get; set; }
        public List<Mails> ReciverMessages { get; set; }
    }
}
