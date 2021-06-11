using Microsoft.EntityFrameworkCore;
using Notification.API.Data.Entities_Type_Configuration;

namespace Notification.API.Data
{
    public class NotificationContext : DbContext
    {
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<Recipient> Recipients { get; set; }

        public NotificationContext(DbContextOptions<NotificationContext> options) : base(options)
        {
            if (Database.ProviderName != "Microsoft.EntityFrameworkCore.InMemory")
                Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new EventEntityTypeConfiguration().Configure(modelBuilder.Entity<Event>());
            new RecipientEntityTypeConfiguration().Configure(modelBuilder.Entity<Recipient>());
        }
    }
}