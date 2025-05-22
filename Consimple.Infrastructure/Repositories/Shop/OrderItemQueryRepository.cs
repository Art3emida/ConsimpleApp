namespace Consimple.Infrastructure.Repositories.Shop;

using Consimple.Application.Dto.Shop;
using Consimple.Application.Contracts.Repositories.Shop;
using Consimple.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

public class OrderItemQueryRepository : IOrderItemQueryRepository
{
    private readonly MasterDbContext _context;

    public OrderItemQueryRepository(MasterDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<CustomerCategoryDto>> GetCustomerCategoriesAsync(int customerId)
    {
        return await _context.OrderItems
            .Where(i => i.Order.CustomerId == customerId)
            .GroupBy(i => i.Product.Category)
            .Select(g => new CustomerCategoryDto
            {
                Category = g.Key,
                TotalQuantity = g.Sum(i => i.Quantity)
            })
            .OrderByDescending(dto => dto.TotalQuantity)
            .ToListAsync();
    }
}