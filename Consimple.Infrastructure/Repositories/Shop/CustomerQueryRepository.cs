namespace Consimple.Infrastructure.Repositories.Shop;

using Microsoft.EntityFrameworkCore;
using Consimple.Application.Dto.Shop;
using Consimple.Application.Contracts.Repositories.Shop;
using Consimple.Infrastructure.Persistence.Context;

public class CustomerQueryRepository : ICustomerQueryRepository
{
    private readonly MasterDbContext _context;

    public CustomerQueryRepository(MasterDbContext context)
    {
        _context = context;
    }

    public async Task<bool> ExistsByIdAsync(int id)
    {
        return await _context.Customers.AnyAsync(c => c.Id == id);
    }

    public async Task<IEnumerable<CustomerBirthdayDto>> GetBirthdayCustomersAsync(DateOnly date)
    {
        return await _context.Customers
            .Where(c => c.BirthDate.Year == date.Year && c.BirthDate.Month == date.Month && c.BirthDate.Day == date.Day)
            .OrderBy(c => c.Id)
            .Select(c => new CustomerBirthdayDto
            {
                Id = c.Id,
                FullName = c.FullName
            })
            .ToListAsync();
    }

    public async Task<IEnumerable<RecentBuyerDto>> GetRecentBuyersAsync(int days)
    {
        var sinceDate = DateTime.UtcNow.AddDays(-days);

        return await _context.Orders
            .Where(o => o.Date >= sinceDate)
            .GroupBy(o => o.Customer)
            .Select(g => new RecentBuyerDto
            {
                Id = g.Key.Id,
                FullName = g.Key.FullName,
                LastOrderDate = g.Max(o => o.Date)
            })
            .OrderByDescending(dto => dto.LastOrderDate)
            .ToListAsync();
    }
}