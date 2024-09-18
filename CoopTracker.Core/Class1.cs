using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CoopTracker.Core;

internal class MyContextFactory : IDesignTimeDbContextFactory<DbContext>
{
    public DbContext CreateDbContext(string[] args)
    {
        var dbContextBuilder = new DbContextOptionsBuilder<DbContext>();
        var connString = "myconnection string";
        dbContextBuilder.UseSqlServer(connString);
        return new DbContext(dbContextBuilder.Options);
    }
}

