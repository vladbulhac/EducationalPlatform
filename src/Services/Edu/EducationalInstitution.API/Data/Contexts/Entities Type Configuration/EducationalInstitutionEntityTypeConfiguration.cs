using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationalInstitutionAPI.Data.Contexts.Entities_Type_Configuration
{
    public class EducationalInstitutionEntityTypeConfiguration : IEntityTypeConfiguration<EducationalInstitution>
    {
        public void Configure(EntityTypeBuilder<EducationalInstitution> builder)
        {
            builder.HasKey(ei => ei.EducationalInstitutionID);
            builder.HasIndex(ei => new { ei.EducationalInstitutionID, ei.IsDisabled }).IsUnique();
            builder.HasIndex(ei => new { ei.LocationID, ei.EducationalInstitutionID, ei.IsDisabled }).IsUnique();
            builder.HasIndex(ei => new { ei.Name, ei.LocationID, ei.IsDisabled, ei.EducationalInstitutionID, ei.Description }).IsUnique();
            builder.HasIndex(ei => ei.Name).IsUnique();

            builder.HasOne(ei => ei.ParentInstitution)
                    .WithMany(ei => ei.ChildInstitutions);

            builder.HasMany(ei => ei.Buildings)
                   .WithOne(b => b.EducationalInstitution)
                   .HasForeignKey(eib => eib.EducationalInstitutionID)
                   .IsRequired();

            builder.HasMany(ei => ei.Admins)
                .WithOne(a => a.EducationalInstitution)
                .HasForeignKey(eia => eia.EducationalInstitutionID)
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