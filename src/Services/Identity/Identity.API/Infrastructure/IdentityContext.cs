using Identity.API.Infrastructure.Entity_Type_Configuration;
using Identity.API.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Identity.API.Infrastructure
{
    public class IdentityContext : IdentityDbContext<User>
    {
        public DbSet<EducationalInstitutionAdministrator> EducationalInstitutionAdministrators { get; set; }
        public DbSet<UserPermissions> Permissions { get; set; }

        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new UserEntityTypeConfiguration());
            builder.ApplyConfiguration(new UserPermissionsEntityTypeConfiguration());
            builder.ApplyConfiguration(new EducationalInstitutionAdministratorEntityTypeConfiguration());
        }
    }
}