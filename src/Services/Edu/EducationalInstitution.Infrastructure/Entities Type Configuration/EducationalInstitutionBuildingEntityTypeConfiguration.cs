using EducationalInstitution.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationalInstitution.Infrastructure.Entities_Type_Configuration;

public class EducationalInstitutionBuildingEntityTypeConfiguration : IEntityTypeConfiguration<EducationalInstitutionBuilding>
{
    public void Configure(EntityTypeBuilder<EducationalInstitutionBuilding> builder)
    {
        builder.HasKey(eib => new { eib.Id, eib.EducationalInstitutionId });

        builder.Property(eib => eib.EducationalInstitutionId).HasColumnName("EducationalInstitutionID");

        builder.OwnsOne(eib => eib.Access, exp =>
        {
            exp.HasIndex(eib => new { eib.IsDisabled });
            exp.Property(a => a.IsDisabled).HasColumnName("IsDisabled");
            exp.Property(a => a.DateForPermanentDeletion).HasColumnName("DateForPermanentDeletion");
        });
    }
}