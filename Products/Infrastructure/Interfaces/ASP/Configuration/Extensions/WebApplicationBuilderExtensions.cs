using Products.Application.ACL;
using Products.Application.Internal.CommandServices;
using Products.Application.Internal.QueryServices;
using Products.Domain.Repositories;
using Products.Domain.Services;
using Products.Domain.Services.QueryServices;
using Products.Infrastructure.Persistence.EFC.Repositories;
using Products.Interfaces.ACL;

namespace Products.Infrastructure.Interfaces.ASP.Configuration.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static IServiceCollection AddProductsContextServices(this IServiceCollection services)
    {
        // Query Services
        services.AddScoped<IProductQueryService, ProductQueryService>();
        services.AddScoped<IBrandQueryService, BrandQueryService>();
        services.AddScoped<ICategoryQueryService, CategoryQueryService>();
        services.AddScoped<ILaboratoryQueryService, LaboratoryQueryService>();
        services.AddScoped<ISupplierQueryService, SupplierQueryService>();
        services.AddScoped<IStockEntryQueryService, StockEntryQueryService>();
        services.AddScoped<IScheduledDiscountQueryService, ScheduledDiscountQueryService>();
        services.AddScoped<ISalesReportQueryService, SalesReportQueryService>();
        services.AddScoped<IEntriesReportQueryService, EntriesReportQueryService>();
        services.AddScoped<IStockReportQueryService, StockReportQueryService>();

        // Command Services
        services.AddScoped<IProductCommandService, ProductCommandService>();
        services.AddScoped<IBrandCommandService, BrandCommandService>();
        services.AddScoped<ICategoryCommandService, CategoryCommandService>();
        services.AddScoped<ILaboratoryCommandService, LaboratoryCommandService>();
        services.AddScoped<ISupplierCommandService, SupplierCommandService>();
        services.AddScoped<IDiscountsCommandService, DiscountsCommandService>();
        services.AddScoped<IScheduledDiscountCommandService, ScheduledDiscountCommandService>();

        // Repositories
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IBrandRepository, BrandRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ILaboratoryRepository, LaboratoryRepository>();
        services.AddScoped<ISupplierRepository, SupplierRepository>();
        services.AddScoped<IStockEntryRepository, StockEntryRepository>();
        services.AddScoped<IProductDiscountRepository, ProductDiscountRepository>();

        // ACL Facade
        services.AddScoped<IProductsContextFacade, ProductsContextFacade>();

        return services;
    }
}