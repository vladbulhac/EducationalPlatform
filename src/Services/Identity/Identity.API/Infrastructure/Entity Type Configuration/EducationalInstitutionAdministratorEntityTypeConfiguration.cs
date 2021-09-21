using Identity.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.API.Infrastructure.Entity_Type_Configuration
{
    public class EducationalInstitutionAdministratorEntityTypeConfiguration : IEntityTypeConfiguration<EducationalInstitutionAdministrator>
    {
        public void Configure(EntityTypeBuilder<EducationalInstitutionAdministrator> builder)
        {
            builder.HasKey(eia => new { eia.UserId, eia.EducationalInstitutionId });

            builder.HasOne(eia => eia.User)
                   .WithMany()
                   .HasForeignKey(eia => eia.UserId);
        }
    }
}