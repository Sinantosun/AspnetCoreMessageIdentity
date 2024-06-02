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
        public DbSet<ForwadMails> ForwadMails { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Mails>()
                .HasOne(m => m.Sender).WithMany(u => u.SentMessages).HasForeignKey(x => x.SenderId).OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Mails>()
            .HasOne(m => m.Receiver).WithMany(u => u.ReciverMessages).HasForeignKey(x => x.ReceiverId).OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ReplyMails>()
           .HasOne(m => m.Mails).WithMany(u => u.ReplyMails).HasForeignKey(x => x.MailsId).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ForwadMails>()
            .HasOne(m => m.Mails).WithMany(u => u.ForwadMails).HasForeignKey(x => x.MailsId).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ForwadMails>()
         .HasOne(m => m.ReciverUser).WithMany(u => u.ForwardReciver).HasForeignKey(x => x.ReciverID).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ForwadMails>()
        .HasOne(m => m.SenderUser).WithMany(u => u.ForwardSender).HasForeignKey(x => x.SenderID).OnDelete(DeleteBehavior.Restrict);


            builder.Entity<ForwadMails>()
        .HasOne(m => m.OldUser).WithMany(u => u.ForwadOldUser).HasForeignKey(x => x.OldUserID).OnDelete(DeleteBehavior.Restrict);

        }
    }
}
