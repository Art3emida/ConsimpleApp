namespace Consimple.Application.Services.Shop;

using Consimple.Application.Contracts.Repositories.Shop;
using Consimple.Application.Dto.Shop;
using Consimple.Application.Services.Shop.Interfaces;

public class ShopQueryService : IShopQueryService
{
    private readonly ICustomerQueryRepository _customerRepository;
    private readonly IOrderItemQueryRepository _orderItemRepository;

    public ShopQueryService(
        ICustomerQueryRepository customerRepository,
        IOrderItemQueryRepository orderItemRepository
    ) {
        _customerRepository = customerRepository;
        _orderItemRepository = orderItemRepository;
    }

    public Task<IEnumerable<CustomerBirthdayDto>> GetBirthdayCustomersAsync(DateOnly date)
    {
        return _customerRepository.GetBirthdayCustomersAsync(date);
    }

    public Task<IEnumerable<RecentBuyerDto>> GetRecentBuyersAsync(int days)
    {
        return _customerRepository.GetRecentBuyersAsync(days);
    }

    public Task<IEnumerable<CustomerCategoryDto>> GetCustomerCategoriesAsync(int customerId)
    {
        return _orderItemRepository.GetCustomerCategoriesAsync(customerId);
    }

    public Task<bool> CustomerExistsByIdAsync(int id)
    {
        return _customerRepository.ExistsByIdAsync(id);
    }
}