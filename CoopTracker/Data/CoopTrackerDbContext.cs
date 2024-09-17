using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CoopTracker;

public partial class CoopTrackerDbContext : DbContext
{

    public class Student {
        public required int StudentId { get; set; }
        public required string StudentGeorgianCoolegeId { get; set; }
        public required string Email { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public LinkedList<Trackee>? Trackee { get; set; }
    }
    public class Tracker
    {
        public required int TrackerId { get; set; }
        public required DateTime Start { get; set; }
        public required DateTime End { get; set; }
        public required DateTime Submit { get; set; }
        public LinkedList<Trackee>? Trackee { get; set; }
    }

    public class Trackee
    {
        public required int TrackeeId { get; set; }
        public required Tracker Tracker { get; set; }
        public required Student Student { get; set; }
        public required string CompanyName { get; set; }
        public required string CompanyCity { get; set; }
        public required string JobTitle { get; set; }
        public required DateTime DateAppliation { get; set; }
        public required string DocumentProvided { get; set; }
        public required string LastUpdate { get; set; }
    }

    public class TrackerDetail
    {
        public required int TrackerDetailId { get; set; }
        public required string StudentId { get; set; }
        public required string CompanyName { get; set; }
        public required string CompanyCity { get; set; }
        public required string JobTitle { get; set; }
        public required DateTime DateAppliation { get; set; }
        public required string DocumentProvided { get; set; }
        public required string LastUpdate { get; set; }
    }


    


    
    public DbSet<Student> Students { get; set; }
    public DbSet<Trackee> Trackees { get; set; }
    public DbSet<Tracker> Trackers { get; set; }


    public DbSet<TrackerDetail> TrackerDetails { get; set; }

    public CoopTrackerDbContext()
    {
    }

    public CoopTrackerDbContext(DbContextOptions<CoopTrackerDbContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
