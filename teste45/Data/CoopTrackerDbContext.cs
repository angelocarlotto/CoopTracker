using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace teste45.Data;

public partial class CoopTrackerDbContext : DbContext
{

    private readonly string _tenant_id;
    public CoopTrackerDbContext()
    {
    }

    public CoopTrackerDbContext(DbContextOptions<CoopTrackerDbContext> options,string tenantId)
        : base(options)
    {
        _tenant_id = tenantId;
    }

    public virtual DbSet<ProffApply> ProffApplys { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<Trackee> Trackees { get; set; }

    public virtual DbSet<Tracker> Trackers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProffApply>(entity =>
        {
            entity.HasIndex(e => e.TrackeeId, "IX_ProffApplys_TrackeeId");

            entity.HasOne(d => d.Trackee).WithMany(p => p.ProffApplies).HasForeignKey(d => d.TrackeeId);

            entity.HasQueryFilter(e=>e.TenantId==_tenant_id)
        });

        modelBuilder.Entity<Trackee>(entity =>
        {
            entity.HasIndex(e => e.StudentId, "IX_Trackees_StudentId");

            entity.HasIndex(e => e.TrackerId, "IX_Trackees_TrackerId");

            entity.HasOne(d => d.Student).WithMany(p => p.Trackees).HasForeignKey(d => d.StudentId);

            entity.HasOne(d => d.Tracker).WithMany(p => p.Trackees).HasForeignKey(d => d.TrackerId);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
