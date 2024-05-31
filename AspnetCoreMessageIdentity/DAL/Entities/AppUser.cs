using Microsoft.AspNetCore.Identity;

namespace AspnetCoreMessageIdentity.DAL.Entities
{
    public class AppUser : IdentityUser<int>
    {
        public string NameSurname { get; set; }
    }
}
