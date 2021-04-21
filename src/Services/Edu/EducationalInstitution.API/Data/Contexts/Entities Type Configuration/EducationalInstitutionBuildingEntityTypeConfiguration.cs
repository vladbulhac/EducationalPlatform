using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationalInstitutionAPI.Data.Contexts.Entities_Type_Configuration
{
    public class EducationalInstitutionBuildingEntityTypeConfiguration : IEntityTypeConfiguration<EducationalInstitutionBuilding>
    {
        public void Configure(EntityTypeBuilder<EducationalInstitutionBuilding> builder)
        {
            builder.OwnsOne(ei => ei.EntityAccess, accessibility =>
            {
                accessibility.Property(a => a.IsDisabled).IsRequired();
                accessibility.HasIndex(a => new { a.DateForPermanentDeletion, a.IsDisabled });
            });

            builder.HasKey(eib => new { eib.BuildingID, eib.EducationalInstitutionID });
        }
    }
}