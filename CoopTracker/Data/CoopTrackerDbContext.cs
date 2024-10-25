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
        // Assuming TenantId is in session
        if (context.Session.GetString("TenantId") == null)
        {
            // Set default TenantId if not set
            context.Session.SetString("TenantId", string.Empty);
        }

          // Check if the "KeyTenant" header is present
       else  if (context.Request.Headers.TryGetValue("TenantId", out var keyTenant))
        {
            // You can now use the keyTenant value as needed
            // For example, you can set it in session
            context.Session.SetString("TenantId", keyTenant.ToString());
        }
        // else
        // {
        //     // Handle the case where KeyTenant is missing
        //     context.Response.StatusCode = StatusCodes.Status400BadRequest; // Bad Request
        //     await context.Response.WriteAsync("KeyTenant header is required.");
        //     return; // Exit the middleware chain
        // }

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
