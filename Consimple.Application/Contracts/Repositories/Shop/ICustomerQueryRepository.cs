namespace Consimple.Application.Contracts.Repositories.Shop;

using Consimple.Application.Dto.Shop;

public interface ICustomerQueryRepository
{
    Task<bool> ExistsByIdAsync(int id);

    Task<IEnumerable<CustomerBirthdayDto>> GetBirthdayCustomersAsync(DateOnly date);

    Task<IEnumerable<RecentBuyerDto>> GetRecentBuyersAsync(int days);
}
