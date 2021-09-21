using Identity.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.API.Infrastructure.Entity_Type_Configuration
{
    public class UserPermissionsEntityTypeConfiguration : IEntityTypeConfiguration<UserPermissions>
    {
        public void Configure(EntityTypeBuilder<UserPermissions> builder)
        {
            builder.HasKey(up => up.UserId);

            builder.HasMany(up => up.EducationalInstitutionAdminPermissions)
                   .WithOne();

            builder.HasOne(up => up.User)
                   .WithOne(u => u.Permissions)
                   .HasForeignKey<UserPermissions>(up => up.UserId);
        }
    }
}