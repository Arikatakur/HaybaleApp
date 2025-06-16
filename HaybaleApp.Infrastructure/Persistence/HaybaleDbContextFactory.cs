using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace HaybaleApp.Infrastructure.Persistence;

public class HaybaleDbContextFactory : IDesignTimeDbContextFactory<HaybaleDbContext>
{
    public HaybaleDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<HaybaleDbContext>();

        optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=HaybaleDb;Trusted_Connection=True;");

        return new HaybaleDbContext(optionsBuilder.Options);
    }
}
