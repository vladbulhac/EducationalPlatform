using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationalInstitutionAPI.Data.Contexts.Entities_Type_Configuration
{
    public class EducationalInstitutionAdminEntityTypeConfiguration : IEntityTypeConfiguration<EducationalInstitutionAdmin>
    {
        public void Configure(EntityTypeBuilder<EducationalInstitutionAdmin> builder)
        {
            builder.HasKey(eia => new { eia.AdminID, eia.EducationalInstitutionID });
            builder.HasIndex(eia => new { eia.EducationalInstitutionID, eia.IsDisabled, eia.AdminID }).IsUnique();
        }
    }
}