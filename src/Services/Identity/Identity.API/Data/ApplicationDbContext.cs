using Identity.API.Models;
using Identity.API.Models.UserSettings;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Identity.API.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ApplicationUser> AppUsers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Professor> Professors { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<UserSettings> UsersSettings { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            SetUserConstraints(builder);
            SetUserTableIndexes(builder);

            SetStudentConstraints(builder);
            SetStudentTableIndexes(builder);

            SetProfessorConstraints(builder);
            SetProfessorTableIndexes(builder);

            SetUserSettingsConstraints(builder);
        }

        #region User table constraints and indexes

        private static void SetUserConstraints(ModelBuilder builder)
        {
            builder.Entity<ApplicationUser>()
                        .Property(au => au.FirstName)
                        .HasMaxLength(30)
                        .IsRequired();
            builder.Entity<ApplicationUser>()
                        .Property(au => au.LastName)
                        .HasMaxLength(30)
                        .IsRequired();
            builder.Entity<ApplicationUser>()
                        .Property(au => au.Country)
                        .HasMaxLength(30)
                        .IsRequired();
            builder.Entity<ApplicationUser>()
                        .Property(au => au.Description)
                        .HasMaxLength(300)
                        .IsRequired(false);
            builder.Entity<ApplicationUser>()
                        .Property(au => au.Skills)
                        .HasMaxLength(30)
                        .IsRequired(false);
            builder.Entity<ApplicationUser>()
                        .Property(au => au.Interests)
                        .HasMaxLength(10)
                        .IsRequired(false);
            builder.Entity<ApplicationUser>()
                        .Property(au => au.Languages)
                        .HasMaxLength(25)
                        .IsRequired();
            builder.Entity<ApplicationUser>()
                        .Property(au => au.JoinDate)
                        .HasDefaultValueSql("getdate()")
                        .ValueGeneratedOnAdd()
                        .IsRequired();
            builder.Entity<ApplicationUser>()
                        .Property(au => au.Birthdate)
                        .IsRequired();
            builder.Entity<ApplicationUser>()
                        .Property(au => au.LastUpdate)
                        .HasDefaultValueSql("getdate()")
                        .ValueGeneratedOnAddOrUpdate()
                        .IsRequired();
            builder.Entity<ApplicationUser>()
                        .HasOne(au => au.Settings)
                        .WithOne()
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
            builder.Entity<ApplicationUser>()
                        .HasOne(au => au.ProfessorDetails)
                        .WithOne()
                        .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<ApplicationUser>()
                        .HasOne(au => au.StudentDetails)
                        .WithOne()
                        .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<ApplicationUser>()
                        .HasOne(au => au.AdminDetails)
                        .WithOne()
                        .OnDelete(DeleteBehavior.Cascade);
        }

        private static void SetUserTableIndexes(ModelBuilder builder)
        {
            builder.Entity<ApplicationUser>()
                            .HasIndex(au => new { au.Email, au.PasswordHash });
            builder.Entity<ApplicationUser>()
                           .HasIndex(au => new { au.LastName, au.FirstName, au.Country });
        }

        #endregion User table constraints and indexes

        #region Student table constraints and indexes

        private static void SetStudentConstraints(ModelBuilder builder)
        {
            builder.Entity<Student>()
                    .Property(s => s.EndDate)
                    .IsRequired();
            builder.Entity<Student>()
                    .Property(s => s.StartDate)
                    .IsRequired();
            builder.Entity<Student>()
                    .Property(s => s.SchoolId)
                    .IsRequired();
            builder.Entity<Student>()
                    .Property(s => s.Year)
                    .IsRequired();
        }

        private static void SetStudentTableIndexes(ModelBuilder builder)
        {
            builder.Entity<Student>()
                    .HasIndex(s => new { s.SchoolId, s.Year, s.StartDate, s.EndDate });
        }

        #endregion Student table constraints and indexes

        #region Professor table constraints and indexes

        private static void SetProfessorConstraints(ModelBuilder builder)
        {
            builder.Entity<Professor>()
                    .HasKey(p => p.Id);
            builder.Entity<Professor>()
                    .Property(p => p.OfficeId)
                    .IsRequired(false);
            builder.Entity<Professor>()
                    .Property(p => p.Rank)
                    .IsRequired();
            builder.Entity<Professor>()
                    .Property(p => p.SchoolsIds)
                    .HasMaxLength(30)
                    .IsRequired();
            builder.Entity<Professor>()
                    .Property(p => p.StartDate)
                    .IsRequired();
        }

        private static void SetProfessorTableIndexes(ModelBuilder builder)
        {
            builder.Entity<Professor>()
                        .HasIndex(p => new { p.SchoolsIds, p.Rank });
        }

        #endregion Professor table constraints and indexes

        #region UserSettings table constraints

        private static void SetUserSettingsConstraints(ModelBuilder builder)
        {
            builder.Entity<UserSettings>()
                        .HasKey(us => us.UserId);
            builder.Entity<UserSettings>()
                        .Property(us => us.BirthdateVisibility)
                        .IsRequired();
            builder.Entity<UserSettings>()
                        .Property(us => us.EmailVisibility)
                        .IsRequired();
            builder.Entity<UserSettings>()
                        .Property(us => us.PhoneNumberVisibility)
                        .IsRequired();
            builder.Entity<UserSettings>()
                        .Property(us => us.CanGetMessagesFromStrangers)
                        .IsRequired();
            builder.Entity<UserSettings>()
                        .Property(us => us.IgnoreList)
                        .HasMaxLength(100)
                        .IsRequired();
        }

        #endregion UserSettings table constraints
    }
}