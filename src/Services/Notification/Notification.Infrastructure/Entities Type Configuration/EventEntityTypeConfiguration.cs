using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notification.Domain.Models.Aggregates;

namespace Notification.Infrastructure.Entities_Type_Configuration;

public class EventEntityTypeConfiguration : IEntityTypeConfiguration<Event>
{
    public void Configure(EntityTypeBuilder<Event> builder)
    {
        builder.HasKey(e => e.Id);
        builder.HasIndex(e => new { e.Name, e.Message, e.Uri });

        builder.OwnsOne(e => e.TriggerDetails, onb =>
        {
            onb.Property(e => e.Action).HasColumnName("TriggeredByAction")
                                       .HasMaxLength(100)
                                       .IsRequired();

            onb.Property(e => e.Issuer).HasColumnName("Issuer")
                                       .HasMaxLength(150)
                                       .IsRequired();

            onb.Property(e => e.TimeIssued).HasColumnName("TimeIssued")
                                           .IsRequired();

            onb.HasIndex(e => new { e.Issuer, e.TimeIssued, e.Action });
        });

        builder.HasMany(e => e.Recipients)
                .WithOne(pe => pe.Event)
                .HasForeignKey(pe => pe.EventId)
                .OnDelete(DeleteBehavior.Cascade);

        builder.Property(e => e.Name)
                .HasMaxLength(100)
                .IsRequired();
        builder.Property(e => e.Message)
                .HasMaxLength(350)
                .IsRequired();
        builder.Property(e => e.Uri)
                .HasMaxLength(100)
                .IsRequired();
    }
}