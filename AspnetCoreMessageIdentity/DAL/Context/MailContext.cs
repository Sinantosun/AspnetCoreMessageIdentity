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
        public DbSet<MailTags> MailTags { get; set; }
        public DbSet<ReplyMails> replyMails { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Mails>()
                .HasOne(m => m.Sender).WithMany(u => u.SentMessages).HasForeignKey(x => x.SenderId).OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Mails>()
            .HasOne(m => m.Receiver).WithMany(u => u.ReciverMessages).HasForeignKey(x => x.ReceiverId).OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ReplyMails>()
           .HasOne(m => m.Mails).WithMany(u => u.ReplyMails).HasForeignKey(x => x.ReplyMailsID).OnDelete(DeleteBehavior.Cascade);

        }
    }
}
