using Products.Application.ACL;
using Products.Application.Internal.CommandServices;
using Products.Application.Internal.QueryServices;
using Products.Domain.Repositories;
using Products.Domain.Services;
using Products.Infrastructure.Persistence.EFC.Repositories;
using Products.Interfaces.ACL;

namespace Products.Infrastructure.Interfaces.ASP.Configuration.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static IServiceCollection AddProductsContextServices(this IServiceCollection services)
    {
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IBrandRepository, BrandRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ILaboratoryRepository, LaboratoryRepository>();
        services.AddScoped<ISupplierRepository, SupplierRepository>();
        services.AddScoped<IStockEntryRepository, StockEntryRepository>();
        services.AddScoped<IProductCommandService, ProductCommandService>();
        services.AddScoped<IProductQueryService, ProductQueryService>();
        services.AddScoped<IBrandCommandService, BrandCommandService>();
        services.AddScoped<IBrandQueryService, BrandQueryService>();
        services.AddScoped<ICategoryCommandService, CategoryCommandService>();
        services.AddScoped<ICategoryQueryService, CategoryQueryService>();
        services.AddScoped<ILaboratoryCommandService, LaboratoryCommandService>();
        services.AddScoped<ILaboratoryQueryService, LaboratoryQueryService>();
        services.AddScoped<ISupplierCommandService, SupplierCommandService>();
        services.AddScoped<ISupplierQueryService, SupplierQueryService>();
        services.AddScoped<IStockEntryQueryService, StockEntryQueryService>();
        services.AddScoped<IProductsContextFacade, ProductsContextFacade>();
        services.AddScoped<IScheduledDiscountCommandService, ScheduledDiscountCommandService>();
        services.AddScoped<IProductDiscountQueryService, ProductDiscountQueryService>();
        services.AddScoped<IProductDiscountRepository, ProductDiscountRepository>();

        return services;
    }
}