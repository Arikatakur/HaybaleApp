using HaybaleApp.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace HaybaleApp.Infrastructure.Persistence;

public class HaybaleDbContext : DbContext
{
    public HaybaleDbContext(DbContextOptions<HaybaleDbContext> options)
        : base(options)
    {
    }

    public DbSet<Driver> Drivers { get; set; }
    public DbSet<HaybaleOrder> HaybaleOrders { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<ChangeLog> ChangeLogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Driver>().ToTable("Drivers");
        modelBuilder.Entity<HaybaleOrder>().ToTable("HaybaleOrders");
        modelBuilder.Entity<Customer>().ToTable("Customers");
        modelBuilder.Entity<ChangeLog>().ToTable("ChangeLogs");
        
        modelBuilder.Entity<Driver>().HasData(
        new Driver { Id = 1, FullName = "Driver One", Username = "driver1" },
        new Driver { Id = 2, FullName = "Driver Two", Username = "driver2" });
        
    }
}
