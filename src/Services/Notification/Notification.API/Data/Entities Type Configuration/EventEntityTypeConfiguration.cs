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
        }
    }
}