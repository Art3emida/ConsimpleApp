namespace Consimple.Application.Services.Shop.Interfaces;

using Consimple.Application.Dto.Shop;

public interface IShopQueryService
{
    Task<IEnumerable<CustomerBirthdayDto>> GetBirthdayCustomersAsync(DateOnly date);

    Task<IEnumerable<RecentBuyerDto>> GetRecentBuyersAsync(int days);

    Task<IEnumerable<CustomerCategoryDto>> GetCustomerCategoriesAsync(int customerId);

    Task<bool> CustomerExistsByIdAsync(int id);
}