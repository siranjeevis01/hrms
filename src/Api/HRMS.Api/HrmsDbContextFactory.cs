using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace HRMS.Api;

public class HrmsDbContextFactory : IDesignTimeDbContextFactory<HrmsDbContext>
{
    public HrmsDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<HrmsDbContext>();
        optionsBuilder.UseNpgsql("Host=localhost;Database=hrms;Username=postgres;Password=postgres");
        return new HrmsDbContext(optionsBuilder.Options);
    }
}
