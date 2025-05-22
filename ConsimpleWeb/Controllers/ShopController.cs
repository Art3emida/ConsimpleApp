namespace ConsimpleWeb.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Consimple.Application.Dto.Shop;
using Consimple.Application.Services.Shop.Interfaces;

[ApiController]
[Route("api/v1/shop")]
public class ShopController : ControllerBase
{
    private readonly IShopQueryService _shopQueryService;

    public ShopController(IShopQueryService shopQueryService)
    {
        _shopQueryService = shopQueryService;
    }

    [HttpGet("birthday-customers")]
    public async Task<IActionResult> GetBirthdayCustomers([BindRequired] DateOnly date)
    {
        IEnumerable<CustomerBirthdayDto> result = await _shopQueryService.GetBirthdayCustomersAsync(date);

        return Ok(result);
    }

    [HttpGet("recent-buyers")]
    public async Task<IActionResult> GetRecentBuyers([BindRequired] int days)
    {
        IEnumerable<RecentBuyerDto> result = await _shopQueryService.GetRecentBuyersAsync(days);

        return Ok(result);
    }

    [HttpGet("customer-categories/{customerId}")]
    public async Task<IActionResult> GetCustomerCategories([BindRequired] int customerId)
    {
        var exists = await _shopQueryService.CustomerExistsByIdAsync(customerId);
        if (!exists)
            return NotFound(new {
                Error = $"Customer with id {customerId} not found.",
            });
        
        IEnumerable<CustomerCategoryDto> result = await _shopQueryService.GetCustomerCategoriesAsync(customerId);

        return Ok(result);
    }
}