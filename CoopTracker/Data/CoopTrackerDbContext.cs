using System;
using System.Collections.Generic;
using CoopTracker;
using Microsoft.EntityFrameworkCore;

namespace CoopTracker;

public partial class CoopTrackerDbContext : DbContext
{
    public CoopTrackerDbContext()
    {
    }

    public CoopTrackerDbContext(DbContextOptions<CoopTrackerDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<GroupKeyMaster> GroupKeyMasters { get; set; }

    public virtual DbSet<ProffApply> ProffApplys { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<Trackee> Trackees { get; set; }

    public virtual DbSet<Tracker> Trackers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<GroupKeyMaster>(entity =>
        {
            entity.ToTable("GroupKeyMaster");
        });

        modelBuilder.Entity<ProffApply>(entity =>
        {
            entity.HasIndex(e => e.GroupKeyMasterId, "IX_ProffApplys_GroupKeyMasterId");

            entity.HasIndex(e => e.TrackeeId, "IX_ProffApplys_TrackeeId");

            entity.HasOne(d => d.GroupKeyMaster).WithMany(p => p.ProffApply)
                .HasForeignKey(d => d.GroupKeyMasterId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Trackee).WithMany(p => p.ProffApply).HasForeignKey(d => d.TrackeeId);
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasIndex(e => e.GroupKeyMasterId, "IX_Students_GroupKeyMasterId");

            entity.HasOne(d => d.GroupKeyMaster).WithMany(p => p.Student)
                .HasForeignKey(d => d.GroupKeyMasterId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Trackee>(entity =>
        {
            entity.HasIndex(e => e.GroupKeyMasterId, "IX_Trackees_GroupKeyMasterId");

            entity.HasIndex(e => e.StudentId, "IX_Trackees_StudentId");

            entity.HasIndex(e => e.TrackerId, "IX_Trackees_TrackerId");

            entity.HasOne(d => d.GroupKeyMaster).WithMany(p => p.Trackee)
                .HasForeignKey(d => d.GroupKeyMasterId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Student).WithMany(p => p.Trackee).HasForeignKey(d => d.StudentId);

            entity.HasOne(d => d.Tracker).WithMany(p => p.Trackee).HasForeignKey(d => d.TrackerId);
        });

        modelBuilder.Entity<Tracker>(entity =>
        {
            entity.HasIndex(e => e.GroupKeyMasterId, "IX_Trackers_GroupKeyMasterId");

            entity.Property(e => e.Description).HasDefaultValue("");

            entity.HasOne(d => d.GroupKeyMaster).WithMany(p => p.Tracker)
                .HasForeignKey(d => d.GroupKeyMasterId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<TrackerDetail>(entity =>
        {
            entity.Property(e => e.GroupKeyMaster).HasDefaultValue("");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
