using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationalInstitutionAPI.Data.Contexts.Entities_Type_Configuration
{
    /// <summary>
    /// Contains the configuration of the <see cref="EducationalInstitution"/> model
    /// </summary>
    public class EducationalInstitutionEntityTypeConfiguration : IEntityTypeConfiguration<EducationalInstitution>
    {
        public void Configure(EntityTypeBuilder<EducationalInstitution> builder)
        {
            builder.OwnsOne(ei => ei.EntityAccess, access =>
                        {
                            access.Property(a => a.IsDisabled).IsRequired();
                            access.HasIndex(a => new { a.DateForPermanentDeletion, a.IsDisabled });
                        });

            builder.HasKey(ei => ei.EducationalInstitutionID);
            builder.HasIndex(ei => new { ei.LocationID, ei.EducationalInstitutionID });
            builder.HasOne(ei => ei.ParentInstitution)
                    .WithMany(ei => ei.ChildInstitutions);

            builder.HasMany(ei => ei.Buildings)
                   .WithOne(b => b.EducationalInstitution)
                   .HasForeignKey(eib => eib.EducationalInstitutionID)
                   .IsRequired();

            builder.Property(ei => ei.Description)
                        .IsRequired()
                        .HasMaxLength(500);

            builder.Property(ei => ei.Name)
                       .IsRequired()
                       .HasMaxLength(128);

            builder.Property(ei => ei.LocationID)
                        .IsRequired();
        }
    }
}