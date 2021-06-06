using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationalInstitutionAPI.Data.Contexts.Entities_Type_Configuration
{
    public class EducationalInstitutionBuildingEntityTypeConfiguration : IEntityTypeConfiguration<EducationalInstitutionBuilding>
    {
        public void Configure(EntityTypeBuilder<EducationalInstitutionBuilding> builder)
        {
            builder.HasKey(eib => new { eib.BuildingID, eib.EducationalInstitutionID });
            builder.HasIndex(eib => new { eib.BuildingID, eib.EducationalInstitutionID, eib.IsDisabled }).IsUnique();
        }
    }
}