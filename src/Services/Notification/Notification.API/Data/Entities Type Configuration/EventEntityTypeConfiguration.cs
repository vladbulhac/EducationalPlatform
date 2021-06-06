using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Notification.API.Data.Entities_Type_Configuration
{
    public class EventEntityTypeConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.HasKey(e => e.EventID);

            builder.HasMany(e => e.Recipients)
                    .WithOne(pe => pe.Event)
                    .HasForeignKey(pe => pe.EventID);

            builder.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsRequired();
            builder.Property(e => e.Message)
                    .HasMaxLength(350)
                    .IsRequired();
            builder.Property(e => e.TimeIssued)
                    .IsRequired();
            builder.Property(e => e.Url)
                    .HasMaxLength(100)
                    .IsRequired();
            builder.Property(e => e.IssuedBy)
                    .HasMaxLength(100)
                    .IsRequired();
            builder.Property(e => e.TriggeredByAction)
                    .HasMaxLength(50)
                    .IsRequired();
        }
    }
}