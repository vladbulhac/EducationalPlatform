﻿// <auto-generated />
using System;
using EducationalInstitutionAPI.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EducationalInstitutionAPI.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20210531134832_UniqueConstraints")]
    partial class UniqueConstraints
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.6")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EducationalInstitutionAPI.Data.EducationalInstitution", b =>
                {
                    b.Property<Guid>("EducationalInstitutionID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("DateForPermanentDeletion")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<bool>("IsDisabled")
                        .HasColumnType("bit");

                    b.Property<DateTime>("JoinDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("LocationID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<Guid?>("ParentInstitutionEducationalInstitutionID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("EducationalInstitutionID");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("ParentInstitutionEducationalInstitutionID");

                    b.HasIndex("EducationalInstitutionID", "IsDisabled")
                        .IsUnique();

                    b.HasIndex("LocationID", "EducationalInstitutionID", "IsDisabled")
                        .IsUnique();

                    b.HasIndex("Name", "LocationID", "IsDisabled", "EducationalInstitutionID", "Description")
                        .IsUnique();

                    b.ToTable("EducationalInstitutions");
                });

            modelBuilder.Entity("EducationalInstitutionAPI.Data.EducationalInstitutionAdmin", b =>
                {
                    b.Property<Guid>("AdminID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("EducationalInstitutionID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("DateForPermanentDeletion")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDisabled")
                        .HasColumnType("bit");

                    b.HasKey("AdminID", "EducationalInstitutionID");

                    b.HasIndex("EducationalInstitutionID");

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("EducationalInstitutionAPI.Data.EducationalInstitutionBuilding", b =>
                {
                    b.Property<string>("BuildingID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<Guid>("EducationalInstitutionID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("DateForPermanentDeletion")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDisabled")
                        .HasColumnType("bit");

                    b.HasKey("BuildingID", "EducationalInstitutionID");

                    b.HasIndex("EducationalInstitutionID");

                    b.ToTable("Buildings");
                });

            modelBuilder.Entity("EducationalInstitutionAPI.Data.EducationalInstitution", b =>
                {
                    b.HasOne("EducationalInstitutionAPI.Data.EducationalInstitution", "ParentInstitution")
                        .WithMany("ChildInstitutions")
                        .HasForeignKey("ParentInstitutionEducationalInstitutionID");

                    b.Navigation("ParentInstitution");
                });

            modelBuilder.Entity("EducationalInstitutionAPI.Data.EducationalInstitutionAdmin", b =>
                {
                    b.HasOne("EducationalInstitutionAPI.Data.EducationalInstitution", "EducationalInstitution")
                        .WithMany("Admins")
                        .HasForeignKey("EducationalInstitutionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EducationalInstitution");
                });

            modelBuilder.Entity("EducationalInstitutionAPI.Data.EducationalInstitutionBuilding", b =>
                {
                    b.HasOne("EducationalInstitutionAPI.Data.EducationalInstitution", "EducationalInstitution")
                        .WithMany("Buildings")
                        .HasForeignKey("EducationalInstitutionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EducationalInstitution");
                });

            modelBuilder.Entity("EducationalInstitutionAPI.Data.EducationalInstitution", b =>
                {
                    b.Navigation("Admins");

                    b.Navigation("Buildings");

                    b.Navigation("ChildInstitutions");
                });
#pragma warning restore 612, 618
        }
    }
}
