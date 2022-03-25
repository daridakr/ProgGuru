using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Daridakr.ProgGuru.EntityFrameworkCore;

/* This class is needed for EF Core console commands
 * (like Add-Migration and Update-Database commands) */
public class ProgGuruDbContextFactory : IDesignTimeDbContextFactory<ProgGuruDbContext>
{
    public ProgGuruDbContext CreateDbContext(string[] args)
    {
        ProgGuruEfCoreEntityExtensionMappings.Configure();

        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<ProgGuruDbContext>()
            .UseSqlServer(configuration.GetConnectionString("Default"));

        return new ProgGuruDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Daridakr.ProgGuru.DbMigrator/"))
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
