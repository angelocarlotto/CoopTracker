using System.Configuration;
using CoopTracker;
using CoopTracker.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

#if DATABASE_PGSQL
var connectionString = Environment.GetEnvironmentVariable("DefaultConnectionPSQL");
builder.Services.AddDbContext<CoopTrackerDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnectionPSQL")));
#else
var connectionString = Environment.GetEnvironmentVariable("DefaultConnection");
builder.Services.AddDbContext<CoopTrackerDbContext>(options => options.UseSqlServer(string.IsNullOrEmpty(connectionString) ? builder.Configuration.GetConnectionString("DefaultConnection") : connectionString));
#endif

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromDays(90);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

// Add support for both MVC and API controllers
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add(new GroupHashFilter());
});

// Register the API controllers
builder.Services.AddControllers();

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();
app.UseMiddleware<TenantMiddleware>();

// Map routes for both MVC and API
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

// Map API controllers
app.MapControllers();

app.Run();


