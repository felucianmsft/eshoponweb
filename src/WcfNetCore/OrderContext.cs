using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

public class OrderContext : DbContext
{
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseInMemoryDatabase("OrderDb")
            .UseLazyLoadingProxies()
            .LogTo(s => Console.WriteLine(s))
            .EnableDetailedErrors(true)
            .EnableSensitiveDataLogging(true);
    }
}
