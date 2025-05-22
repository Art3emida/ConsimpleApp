namespace Consimple.Application.Contracts.Repositories.Shop;

using Consimple.Application.Dto.Shop;

public interface IOrderItemQueryRepository
{
    Task<IEnumerable<CustomerCategoryDto>> GetCustomerCategoriesAsync(int customerId);
}