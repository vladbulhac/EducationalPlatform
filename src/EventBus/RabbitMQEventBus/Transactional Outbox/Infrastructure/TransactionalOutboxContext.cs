using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RabbitMQEventBus.Transactional_Outbox.Models;
using System;

namespace RabbitMQEventBus.Transactional_Outbox.Infrastructure
{
    public class TransactionalOutboxContext : DbContext
    {
        public DbSet<Outbox> Outbox { get; set; }

        public TransactionalOutboxContext(DbContextOptions<TransactionalOutboxContext> options) : base(options)
        {
            /* if (Database.ProviderName != "Microsoft.EntityFrameworkCore.InMemory")
                 Database.Migrate();*/
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            ConfigureOutboxEntity(builder.Entity<Outbox>());
        }

        private static void ConfigureOutboxEntity(EntityTypeBuilder<Outbox> outboxBuilder)
        {
            outboxBuilder.HasKey(o => o.EventId);

            outboxBuilder.Property(o => o.EventName)
                         .HasMaxLength(256)
                         .IsRequired();

            outboxBuilder.Property(o => o.EventBody)
                         .IsRequired();

            outboxBuilder.Property(o => o.CreatedDate)
                         .IsRequired();

            outboxBuilder.Property(o => o.PublishStatus)
                         .HasConversion(p => p.ToString(),
                                        p => (PublishStatus)Enum.Parse(typeof(PublishStatus), p))
                         .IsRequired();

            outboxBuilder.Property(o => o.TransactionId)
                         .IsRequired();
        }
    }
}