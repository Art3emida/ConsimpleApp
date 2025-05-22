namespace ConsimpleWeb.Extensions;

using Microsoft.EntityFrameworkCore;
using Consimple.Application.Contracts.Repositories.Shop;
using Consimple.Application.Services.Shop;
using Consimple.Application.Services.Shop.Interfaces;
using Consimple.Infrastructure.Persistence.Context;
using Consimple.Infrastructure.Persistence.Initializers;
using Consimple.Infrastructure.Persistence.Initializers.Interfaces;
using Consimple.Infrastructure.Repositories.Shop;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddConsimpleServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddDbContext<MasterDbContext>(
            options => options.UseNpgsql(configuration.GetConnectionString("MasterConnection")));

        services.AddScoped<IDbInitializer, DbInitializer>();

        services.AddScoped<ICustomerQueryRepository, CustomerQueryRepository>();
        services.AddScoped<IOrderItemQueryRepository, OrderItemQueryRepository>();
        services.AddScoped<IShopQueryService, ShopQueryService>();

        return services;
    }
}