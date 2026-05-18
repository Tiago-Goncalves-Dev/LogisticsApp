using Microsoft.VisualBasic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using LogisticsApp.Api.Infrastructure;

namespace LogisticsDbContextFactory;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<LogisticsDbContext>
{
    public LogisticsDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<LogisticsDbContext>();
        optionsBuilder.UseSqlServer("Server=localhost,1433;Database=LogisticsDb;User Id=sa;Password=outubro_1998;TrustServerCertificate=True;");
        return new LogisticsDbContext(optionsBuilder.Options);
    }

}