using EducationaInstitutionAPI.Data.Contexts.Entities_Type_Configuration;
using Microsoft.EntityFrameworkCore;

namespace EducationaInstitutionAPI.Data
{
    /// <summary>
    /// Contains the configuration of each entity
    /// </summary>
    public class DataContext : DbContext
    {
        public virtual DbSet<EduInstitution> EducationalInstitutions { get; set; }
        public virtual DbSet<EduInstitutionBuilding> EducationalInstitutionsBuildings { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DataContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new EduInstitutionBuildingEntityTypeConfiguration().Configure(modelBuilder.Entity<EduInstitutionBuilding>());
            new EduInstitutionEntityTypeConfiguration().Configure(modelBuilder.Entity<EduInstitution>());
        }
    }
}