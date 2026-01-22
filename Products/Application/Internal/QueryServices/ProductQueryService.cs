using Products.Domain.Model.Aggregates;
using Products.Domain.Model.Queries;
using Products.Domain.Repositories;
using Products.Domain.Services;

namespace Products.Application.Internal.QueryServices;

public class ProductQueryService(IProductRepository productRepository) : IProductQueryService
{
    public async Task<Product?> Handle(GetProductByIdQuery query)
    {
        return await productRepository.FindByIdAsync(query.ProductId);
    }

    public async Task<IEnumerable<Product>> Handle(ListProductsQuery query)
    {
        return await productRepository.ListAsync(query.Name);
    }

    public async Task<IEnumerable<Product>> Handle(GetLowStockProductsQuery query)
    {
        return await productRepository.FindLowStockAsync();
    }
}