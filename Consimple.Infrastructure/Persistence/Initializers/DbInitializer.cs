namespace Consimple.Infrastructure.Persistence.Initializers;

using Microsoft.EntityFrameworkCore;
using Consimple.Domain.Model.Shop;
using Consimple.Infrastructure.Persistence.Context;
using Consimple.Infrastructure.Persistence.Initializers.Interfaces;

public class DbInitializer : IDbInitializer
{
    private readonly MasterDbContext _dbContext;

    public DbInitializer(MasterDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task InitializeAsync()
    {
        if ((await _dbContext.Database.GetPendingMigrationsAsync()).Any())
        {
            await _dbContext.Database.MigrateAsync();
        }

        if (!await _dbContext.Products.AnyAsync())
        {
            await SeedData();
        }
    }
    
    private async Task SeedData()
    {
        var customers = new List<Customer>
        {
            new Customer { FullName = "Willy Wonka", BirthDate = new DateOnly(1992, 1, 12), RegistrationDate = DateTime.Now.AddYears(-1) },
            new Customer { FullName = "Bob Catman", BirthDate = new DateOnly(1986, 4, 21), RegistrationDate = DateTime.Now.AddYears(-2) },
            new Customer { FullName = "Britney Hoffer", BirthDate = new DateOnly(1980, 5, 28), RegistrationDate = DateTime.Now.AddMonths(-6) }
        };

        var products = new List<Product>
        {
            new Product { Name = "Notebook White", Category = "Electronics", Code = "PPDF56", Price = 21650 },
            new Product { Name = "Floorstanding Speaker", Category = "Accessories", Code = "MM341", Price = 400 },
            new Product { Name = "Keyboard RGB", Category = "Accessories", Code = "BN34D7", Price = 1220 }
        };

        await _dbContext.Customers.AddRangeAsync(customers);
        await _dbContext.Products.AddRangeAsync(products);
        await _dbContext.SaveChangesAsync();

        var orders = new List<Order>
        {
            new Order { Date = DateTime.UtcNow.AddDays(-7), TotalAmount = 22050, CustomerId = customers[0].Id },
            new Order { Date = DateTime.UtcNow.AddDays(-3), TotalAmount = 2020, CustomerId = customers[1].Id },
            new Order { Date = DateTime.UtcNow.AddDays(-1), TotalAmount = 1220, CustomerId = customers[2].Id }
        };

        await _dbContext.Orders.AddRangeAsync(orders);
        await _dbContext.SaveChangesAsync();

        var orderItems = new List<OrderItem>
        {
            new OrderItem { OrderId = orders[0].Id, ProductId = products[0].Id, Quantity = 1 },
            new OrderItem { OrderId = orders[0].Id, ProductId = products[1].Id, Quantity = 1 },

            new OrderItem { OrderId = orders[1].Id, ProductId = products[1].Id, Quantity = 2 },
            new OrderItem { OrderId = orders[1].Id, ProductId = products[2].Id, Quantity = 1 },

            new OrderItem { OrderId = orders[2].Id, ProductId = products[2].Id, Quantity = 1 }
        };

        await _dbContext.OrderItems.AddRangeAsync(orderItems);
        await _dbContext.SaveChangesAsync();
    }
}
