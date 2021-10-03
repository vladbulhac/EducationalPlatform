using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain = EducationalInstitution.Domain.Models.Aggregates;

namespace EducationalInstitution.Infrastructure.Entities_Type_Configuration
{
    public class EducationalInstitutionEntityTypeConfiguration : IEntityTypeConfiguration<Domain::EducationalInstitution>
    {
        public void Configure(EntityTypeBuilder<Domain::EducationalInstitution> builder)
        {
            builder.HasKey(ei => ei.Id);
            builder.HasIndex(ei => new { ei.LocationID, ei.Id }).IsUnique();
            builder.HasIndex(ei => new { ei.Name, ei.LocationID, ei.Id }).IsUnique();

            builder.Property(eib => eib.Id).HasColumnName("EducationalInstitutionID");

            builder.OwnsOne(ei => ei.Access, exp =>
            {
                exp.HasIndex(ei => new { ei.IsDisabled });
                exp.Property(a => a.IsDisabled).HasColumnName("IsDisabled");
                exp.Property(a => a.DateForPermanentDeletion).HasColumnName("DateForPermanentDeletion");
            });

            builder.HasOne(ei => ei.ParentInstitution)
                   .WithMany(ei => ei.ChildInstitutions);

            builder.HasMany(ei => ei.Buildings)
                   .WithOne(b => b.EducationalInstitution)
                   .HasForeignKey(eib => eib.EducationalInstitutionId)
                   .IsRequired();

            builder.HasMany(ei => ei.Admins)
                   .WithOne(a => a.EducationalInstitution)
                   .HasForeignKey(eia => eia.EducationalInstitutionId)
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