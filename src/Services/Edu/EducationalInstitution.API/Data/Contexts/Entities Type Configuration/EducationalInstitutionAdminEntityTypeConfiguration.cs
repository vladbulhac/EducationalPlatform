using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationalInstitutionAPI.Data.Contexts.Entities_Type_Configuration
{
    public class EducationalInstitutionAdminEntityTypeConfiguration : IEntityTypeConfiguration<EducationalInstitutionAdmin>
    {
        public void Configure(EntityTypeBuilder<EducationalInstitutionAdmin> builder)
        {
            builder.OwnsOne(eia => eia.EntityAccess, accessibility =>
            {
                accessibility.Property(a => a.IsDisabled).IsRequired();
                accessibility.HasIndex(a => new { a.DateForPermanentDeletion, a.IsDisabled });
            });

            builder.HasKey(eia => new { eia.AdminID, eia.EducationalInstitutionID });
        }
    }
}