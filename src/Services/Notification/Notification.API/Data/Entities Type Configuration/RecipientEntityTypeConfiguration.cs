using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Notification.API.Data.Entities_Type_Configuration
{
    public class RecipientEntityTypeConfiguration : IEntityTypeConfiguration<Recipient>
    {
        public void Configure(EntityTypeBuilder<Recipient> builder)
        {
            builder.HasKey(pe => new { pe.EventID, pe.RecipientID });
        }
    }
}