using Microsoft.EntityFrameworkCore;
using Notification.Domain.Models;
using Notification.Domain.Models.Aggregates;
using Notification.Infrastructure.Entities_Type_Configuration;

namespace Notification.Infrastructure
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
            modelBuilder.ApplyConfiguration(new EventEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new RecipientEntityTypeConfiguration());
        }
    }
}