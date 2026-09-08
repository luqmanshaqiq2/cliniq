using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Cliniq.Data;

public sealed class CliniqDbContextFactory : IDesignTimeDbContextFactory<CliniqDbContext>
{
    public CliniqDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<CliniqDbContext>();
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=Cliniq_design;Username=postgres");
        return new CliniqDbContext(optionsBuilder.Options);
    }
}
