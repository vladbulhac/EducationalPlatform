﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Notification.API.Data;

namespace Notification.API.Migrations
{
    [DbContext(typeof(NotificationContext))]
    [Migration("20210516135111_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.6")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Notification.API.Data.Event", b =>
                {
                    b.Property<Guid>("EventID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("IssuedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Message")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("TimeIssued")
                        .HasColumnType("datetime2");

                    b.Property<string>("TriggeredByAction")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EventID");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("Notification.API.Data.Recipient", b =>
                {
                    b.Property<Guid>("EventID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RecipientID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Seen")
                        .HasColumnType("bit");

                    b.HasKey("EventID", "RecipientID");

                    b.ToTable("Recipients");
                });

            modelBuilder.Entity("Notification.API.Data.Recipient", b =>
                {
                    b.HasOne("Notification.API.Data.Event", "Event")
                        .WithMany("Recipients")
                        .HasForeignKey("EventID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Event");
                });

            modelBuilder.Entity("Notification.API.Data.Event", b =>
                {
                    b.Navigation("Recipients");
                });
#pragma warning restore 612, 618
        }
    }
}
