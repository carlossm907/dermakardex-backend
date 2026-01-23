using Products.Domain.Repositoies;
using Products.Domain.Repositories;
using Products.Infrastructure.Persistence.EFC.Repositories;

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

        return services;
    }
}