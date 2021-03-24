using EducationaInstitutionAPI.Data.Helpers;
using EducationaInstitutionAPI.Utils.Enums;
using Microsoft.EntityFrameworkCore;
using System;

namespace EducationaInstitutionAPI.Data
{
    /// <summary>
    /// Contains the configuration of each entity
    /// </summary>
    public class DataContext : DbContext
    {
        public virtual DbSet<EduInstitution> EducationalInstitutions { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Professor> Professors { get; set; }
        public virtual DbSet<Staff> Personnel { get; set; }
        public virtual DbSet<InstitutionAttended> InstitutionsAttended { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DataContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            StudentConfiguration(modelBuilder);
            ProfessorConfiguration(modelBuilder);
            StaffConfiguration(modelBuilder);
            EduInstitutionConfiguration(modelBuilder);
            InstitutionsAttendedConfiguration(modelBuilder);
        }

        private static void InstitutionsAttendedConfiguration(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<InstitutionAttended>()
                        .HasKey(ia => ia.InstitutionAttendedID);

            modelBuilder.Entity<InstitutionAttended>()
                         .HasOne(ia => ia.EducationalInstitution)
                         .WithMany()
                         .IsRequired();
            modelBuilder.Entity<InstitutionAttended>()
                         .Property(ia => ia.StartDate)
                         .IsRequired();
            modelBuilder.Entity<InstitutionAttended>()
                         .Property(ia => ia.EndDate)
                         .IsRequired();

            modelBuilder.Entity<InstitutionAttended>()
                        .Property(i => i.StartYear)
                        .HasConversion(y => y.ToString(), y => (Year)Enum.Parse(typeof(Year), y));
            modelBuilder.Entity<InstitutionAttended>()
                        .Property(i => i.EndYear)
                        .HasConversion(y => y.ToString(), y => (Year)Enum.Parse(typeof(Year), y));
        }

        private static void EduInstitutionConfiguration(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EduInstitution>()
                        .OwnsOne(ei => ei.Availability, accessibility =>
                        {
                            accessibility.Property(a => a.IsDisabled).IsRequired();
                            accessibility.HasIndex(a => new { a.DateForPermanentDeletion, a.IsDisabled });
                        });

            modelBuilder.Entity<EduInstitution>()
                        .HasKey(ei => ei.EduInstitutionID);
            modelBuilder.Entity<EduInstitution>()
                        .HasIndex(ei => new { ei.LocationID, ei.Name, ei.Description, ei.EduInstitutionID });
            modelBuilder.Entity<EduInstitution>()
                        .HasIndex(ei => new { ei.Name, ei.Description, ei.LocationID, ei.BuildingID, ei.EduInstitutionID });

            modelBuilder.Entity<EduInstitution>()
                        .Property(ei => ei.Description)
                        .IsRequired()
                        .HasMaxLength(500);

            modelBuilder.Entity<EduInstitution>()
                       .Property(ei => ei.Name)
                       .IsRequired()
                       .HasMaxLength(128);

            modelBuilder.Entity<EduInstitution>()
                        .Property(ei => ei.LocationID)
                        .IsRequired();

            modelBuilder.Entity<EduInstitution>()
                        .Property(ei => ei.BuildingID)
                        .IsRequired();
        }

        private static void StudentConfiguration(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>()
                       .OwnsOne(s => s.Availability, accessibility =>
                       {
                           accessibility.Property(a => a.IsDisabled).IsRequired();
                           accessibility.HasIndex(a => new { a.DateForPermanentDeletion, a.IsDisabled });
                       });

            modelBuilder.Entity<Student>()
                        .HasKey(s => s.IdentityID);
            modelBuilder.Entity<Student>()
                        .HasIndex(s => new { s.IdentityID, s.CurrentYear }).IsUnique();
        }

        private static void ProfessorConfiguration(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Professor>()
                        .OwnsOne(p => p.Availability, accessibility =>
                        {
                            accessibility.Property(a => a.IsDisabled).IsRequired();
                            accessibility.HasIndex(a => new { a.DateForPermanentDeletion, a.IsDisabled });
                        });

            modelBuilder.Entity<Professor>()
                        .HasKey(p => p.IdentityID);
            modelBuilder.Entity<Professor>()
                        .HasIndex(p => new { p.IdentityID, p.Rank, p.OfficeID }).IsUnique();
            modelBuilder.Entity<Professor>()
                        .HasIndex(p => p.OfficeID);

            modelBuilder.Entity<Professor>()
                        .Property(p => p.OfficeID)
                        .IsRequired();
        }

        private static void StaffConfiguration(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Staff>()
                        .OwnsOne(p => p.Availability, accessibility =>
                        {
                            accessibility.Property(a => a.IsDisabled).IsRequired();
                            accessibility.HasIndex(a => new { a.DateForPermanentDeletion, a.IsDisabled });
                        });

            modelBuilder.Entity<Staff>()
                        .HasKey(p => p.IdentityID);
        }
    }
}