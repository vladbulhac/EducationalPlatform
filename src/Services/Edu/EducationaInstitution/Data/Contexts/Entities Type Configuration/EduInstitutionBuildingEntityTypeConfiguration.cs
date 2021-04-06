using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationaInstitutionAPI.Data.Contexts.Entities_Type_Configuration
{
    /// <summary>
    /// Contains the configuration of the <see cref="EduInstitutionBuilding"/> model
    /// </summary>
    public class EduInstitutionBuildingEntityTypeConfiguration : IEntityTypeConfiguration<EduInstitutionBuilding>
    {
        public void Configure(EntityTypeBuilder<EduInstitutionBuilding> builder)
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