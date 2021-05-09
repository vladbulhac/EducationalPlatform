using EducationalInstitutionAPI.Data.Contexts.Entities_Type_Configuration;
using Microsoft.EntityFrameworkCore;

namespace EducationalInstitutionAPI.Data.Contexts
{
    /// <summary>
    /// Contains the configuration of each entity
    /// </summary>
    public class DataContext : DbContext
    {
        public virtual DbSet<EducationalInstitution> EducationalInstitutions { get; set; }
        public virtual DbSet<EducationalInstitutionBuilding> Buildings { get; set; }
        public virtual DbSet<EducationalInstitutionAdmin> Admins { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        { }

        public DataContext()
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new EducationalInstitutionBuildingEntityTypeConfiguration().Configure(modelBuilder.Entity<EducationalInstitutionBuilding>());
            new EducationalInstitutionEntityTypeConfiguration().Configure(modelBuilder.Entity<EducationalInstitution>());
            new EducationalInstitutionAdminEntityTypeConfiguration().Configure(modelBuilder.Entity<EducationalInstitutionAdmin>());
        }
    }
}