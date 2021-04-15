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
        public virtual DbSet<EducationalInstitutionBuilding> EducationalInstitutionsBuildings { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DataContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new EducationalInstitutionBuildingEntityTypeConfiguration().Configure(modelBuilder.Entity<EducationalInstitutionBuilding>());
            new EducationalInstitutionEntityTypeConfiguration().Configure(modelBuilder.Entity<EducationalInstitution>());
        }
    }
}