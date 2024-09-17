﻿// <auto-generated />
using System;
using CoopTracker;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CoopTracker.Migrations
{
    [DbContext(typeof(CoopTrackerDbContext))]
    partial class CoopTrackerDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CoopTracker.CoopTrackerDbContext+Student", b =>
                {
                    b.Property<int>("StudentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StudentId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StudentGeorgianCoolegeId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StudentId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("CoopTracker.CoopTrackerDbContext+Trackee", b =>
                {
                    b.Property<int>("TrackeeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TrackeeId"));

                    b.Property<string>("CompanyCity")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateAppliation")
                        .HasColumnType("datetime2");

                    b.Property<string>("DocumentProvided")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("JobTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastUpdate")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.Property<int>("TrackerId")
                        .HasColumnType("int");

                    b.HasKey("TrackeeId");

                    b.HasIndex("StudentId");

                    b.HasIndex("TrackerId");

                    b.ToTable("Trackees");
                });

            modelBuilder.Entity("CoopTracker.CoopTrackerDbContext+Tracker", b =>
                {
                    b.Property<int>("TrackerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TrackerId"));

                    b.Property<DateTime>("End")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Start")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Submit")
                        .HasColumnType("datetime2");

                    b.HasKey("TrackerId");

                    b.ToTable("Trackers");
                });

            modelBuilder.Entity("CoopTracker.CoopTrackerDbContext+TrackerDetail", b =>
                {
                    b.Property<int>("TrackerDetailId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TrackerDetailId"));

                    b.Property<string>("CompanyCity")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateAppliation")
                        .HasColumnType("datetime2");

                    b.Property<string>("DocumentProvided")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("JobTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastUpdate")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StudentId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TrackerDetailId");

                    b.ToTable("TrackerDetails");
                });

            modelBuilder.Entity("CoopTracker.CoopTrackerDbContext+Trackee", b =>
                {
                    b.HasOne("CoopTracker.CoopTrackerDbContext+Student", "Student")
                        .WithMany("Trackee")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CoopTracker.CoopTrackerDbContext+Tracker", "Tracker")
                        .WithMany("Trackee")
                        .HasForeignKey("TrackerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Student");

                    b.Navigation("Tracker");
                });

            modelBuilder.Entity("CoopTracker.CoopTrackerDbContext+Student", b =>
                {
                    b.Navigation("Trackee");
                });

            modelBuilder.Entity("CoopTracker.CoopTrackerDbContext+Tracker", b =>
                {
                    b.Navigation("Trackee");
                });
#pragma warning restore 612, 618
        }
    }
}
