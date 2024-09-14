using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace CRM.EntityFrameworkCore;

public class CRMHttpApiHostMigrationsDbContextFactory : IDesignTimeDbContextFactory<CRMHttpApiHostMigrationsDbContext>
{
    public CRMHttpApiHostMigrationsDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<CRMHttpApiHostMigrationsDbContext>()
            .UseNpgsql(configuration.GetConnectionString("CRM"));

        return new CRMHttpApiHostMigrationsDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
