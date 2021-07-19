using EducationalInstitution.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationalInstitution.Infrastructure.Entities_Type_Configuration
{
    public class EducationalInstitutionAdminEntityTypeConfiguration : IEntityTypeConfiguration<EducationalInstitutionAdmin>
    {
        public void Configure(EntityTypeBuilder<EducationalInstitutionAdmin> builder)
        {
            builder.HasKey(eia => new { eia.AdminID, eia.Id });

            builder.Property(eib => eib.Id).HasColumnName("EducationalInstitutionID");

            builder.OwnsOne(eia => eia.Access, exp =>
            {
                exp.HasIndex(eia => new { eia.IsDisabled });
                exp.Property(a => a.IsDisabled).HasColumnName("IsDisabled");
                exp.Property(a => a.DateForPermanentDeletion).HasColumnName("DateForPermanentDeletion");
            });
        }
    }
}