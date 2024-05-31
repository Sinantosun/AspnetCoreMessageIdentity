using AspnetCoreMessageIdentity.DAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AspnetCoreMessageIdentity.DAL.Context
{
    public class MailContext : IdentityDbContext<AppUser, AppRole, int>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=DESKTOP-TOGRPIE\\SQLEXPRESS;database=MailDb;integrated security=true; trustServerCertificate=true");
        }

        public DbSet<Mails> Mail { get; set; }
    }
}
