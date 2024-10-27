using Microsoft.EntityFrameworkCore;

namespace CoopTracker;

public class TenantMiddleware
{
    private readonly RequestDelegate _next;

    public TenantMiddleware(RequestDelegate next)
    {
        _next = next;
    }

        public async Task Invoke(HttpContext context)
    {
        // Skip middleware for API requests
        // if (context.Request.Path.StartsWithSegments("/api"))
        // {
        //     await _next(context);
        //     return;
        // }

        // Check if the "TenantId" header is present
        if (context.Request.Headers.TryGetValue("TenantId", out var keyTenant))
        {
            // If the header is present, update the session with the value from the header
            context.Session.SetString("TenantId", keyTenant.ToString());
        }
        else if (string.IsNullOrEmpty(context.Session.GetString("TenantId")))
        {
            // If the session does not have a TenantId and no header is provided, set it to a default value
            context.Session.SetString("TenantId", "default");
        }

        await _next(context);
    }
}

public interface ITenantBaseEntity
{
    public string TenantId { get; set; }
}


public partial class CoopTrackerDbContext : DbContext
{

    private readonly string _tenantId;

    private readonly IHttpContextAccessor _httpContextAccessor;
    // public CoopTrackerDbContext() { }
    // public CoopTrackerDbContext(DbContextOptions<CoopTrackerDbContext> options) : base(options) { }
    public CoopTrackerDbContext(DbContextOptions<CoopTrackerDbContext> options, IHttpContextAccessor httpContextAccessor)
       : base(options)
    {
        _httpContextAccessor = httpContextAccessor;
        _tenantId = _httpContextAccessor.HttpContext?.Session.GetString("TenantId");
    }

    public virtual DbSet<ProffApply> ProffApplys { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<Trackee> Trackees { get; set; }

    public virtual DbSet<Tracker> Trackers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#if DATABASE_PGSQL
        => optionsBuilder.UseNpgsql("Name=DefaultConnectionPSQL");
#else
        => optionsBuilder.UseSqlServer("Name=DefaultConnection");
#endif


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProffApply>().HasQueryFilter(e => e.TenantId == _tenantId);
        modelBuilder.Entity<Student>().HasQueryFilter(e => e.TenantId == _tenantId);
        modelBuilder.Entity<Trackee>().HasQueryFilter(e => e.TenantId == _tenantId);
        //modelBuilder.Entity<Tracker>().HasQueryFilter(e => e.TenantId == _tenantId);
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);


}
