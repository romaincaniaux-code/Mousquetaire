using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Mousquetaire.Data;

public class PrepOrderDbContextFactory : IDesignTimeDbContextFactory<PrepOrderDbContext>
{
    public PrepOrderDbContext CreateDbContext(string[] args)
    {
        var options = new DbContextOptionsBuilder<PrepOrderDbContext>()
            .UseSqlServer(DatabaseConfiguration.ConnectionString)
            .Options;

        return new PrepOrderDbContext(options);
    }
}
