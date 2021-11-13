using EducationalInstitution.Domain.Models;
using EducationalInstitution.Infrastructure.Entities_Type_Configuration;
using Microsoft.EntityFrameworkCore;

using Aggregate = EducationalInstitution.Domain.Models.Aggregates;

namespace EducationalInstitution.Infrastructure;

/// <summary>
/// Contains the configuration of each entity
/// </summary>
public class DataContext : DbContext
{
    public virtual DbSet<Aggregate::EducationalInstitution> EducationalInstitutions { get; set; }
    public virtual DbSet<EducationalInstitutionBuilding> Buildings { get; set; }
    public virtual DbSet<EducationalInstitutionAdmin> Admins { get; set; }

    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
        /*if (Database.ProviderName != "Microsoft.EntityFrameworkCore.InMemory")
            Database.Migrate();*/
    }

    public DataContext()
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new EducationalInstitutionBuildingEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new EducationalInstitutionEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new EducationalInstitutionAdminEntityTypeConfiguration());
    }
}