namespace Consimple.Infrastructure.Persistence.Context;

using Consimple.Domain.Model.Shop;
using Microsoft.EntityFrameworkCore;

public class MasterDbContext : DbContext
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }

    public MasterDbContext(DbContextOptions<MasterDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.Property(e => e.RegistrationDate)
                .HasColumnType("timestamp without time zone");
        });
    }
}
