using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Cliniq.Data;

public sealed class CliniqDbContextFactory : IDesignTimeDbContextFactory<CliniqDbContext>
{
    public CliniqDbContext CreateDbContext(string[] args)
    {
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true)
            .AddJsonFile($"appsettings.{environment}.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        var connectionString = configuration.GetConnectionString("DefaultConnection");
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            connectionString = "Host=localhost;Port=5432;Database=Cliniq_design;Username=postgres";
        }

        var optionsBuilder = new DbContextOptionsBuilder<CliniqDbContext>();
        optionsBuilder.UseNpgsql(connectionString);
        return new CliniqDbContext(optionsBuilder.Options);
    }
}
