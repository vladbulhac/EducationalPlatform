using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationaInstitutionAPI.Data.Contexts.Entities_Type_Configuration
{
    /// <summary>
    /// Contains the configuration of the <see cref="EduInstitution"/> model
    /// </summary>
    public class EduInstitutionEntityTypeConfiguration : IEntityTypeConfiguration<EduInstitution>
    {
        public void Configure(EntityTypeBuilder<EduInstitution> builder)
        {
            builder.OwnsOne(ei => ei.EntityAccess, access =>
                        {
                            access.Property(a => a.IsDisabled).IsRequired();
                            access.HasIndex(a => new { a.DateForPermanentDeletion, a.IsDisabled });
                        });

            builder.HasKey(ei => ei.EduInstitutionID);
            builder.HasIndex(ei => new { ei.LocationID, ei.EduInstitutionID });
            builder.HasOne(ei => ei.ParentInstitution)
                    .WithMany(ei => ei.ChildInstitutions);

            builder.HasMany(ei => ei.Buildings)
                   .WithOne()
                   .HasForeignKey(eib => eib.EducationalInstitutionID)
                   .IsRequired();

            builder.Property(ei => ei.ParentInstitution)
                .HasColumnName("ParentInstitutionID");

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