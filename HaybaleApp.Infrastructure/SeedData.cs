using HaybaleApp.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace HaybaleApp.Infrastructure.Persistence;

public static class SeedData
{
    public static void Initialize(HaybaleDbContext context)
    {
        if (context.Drivers.Any()) return; // Already seeded

        var drivers = new List<Driver>
        {
            new Driver { Username = "driver1", FullName = "Ali", PhoneNumber = "0501234567" },
            new Driver { Username = "driver2", FullName = "Sami", PhoneNumber = "0507654321" }
        };

        var customers = new List<Customer>
        {
            new Customer { FullName = "Amir", ContactInfo = "0522221111", Address = "Jerusalem" },
            new Customer { FullName = "Dana", ContactInfo = "0544448888", Address = "Tel Aviv" }
        };

        context.Drivers.AddRange(drivers);
        context.Customers.AddRange(customers);
        context.SaveChanges();
    }
}
