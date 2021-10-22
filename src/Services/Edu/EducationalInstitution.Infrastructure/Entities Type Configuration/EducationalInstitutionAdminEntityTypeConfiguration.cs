using EducationalInstitution.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationalInstitution.Infrastructure.Entities_Type_Configuration
{
    public class EducationalInstitutionAdminEntityTypeConfiguration : IEntityTypeConfiguration<EducationalInstitutionAdmin>
    {
        public void Configure(EntityTypeBuilder<EducationalInstitutionAdmin> builder)
        {
            builder.HasKey(eia => new { eia.Id, eia.EducationalInstitutionId });

            builder.Property(eib => eib.EducationalInstitutionId).HasColumnName("EducationalInstitutionID");

            builder.OwnsOne(eia => eia.Access, exp =>
            {
                exp.HasIndex(eia => new { eia.IsDisabled });
                exp.Property(a => a.IsDisabled).HasColumnName("IsDisabled");
                exp.Property(a => a.DateForPermanentDeletion).HasColumnName("DateForPermanentDeletion");
            });

            builder.Property(eia => eia.Permissions)
                    .HasConversion(p => string.Join(';', p),
                                   p => p.Split(';', System.StringSplitOptions.RemoveEmptyEntries))
                    .IsRequired();
        }
    }
}