using Products.Domain.Model.Aggregates;
using Products.Domain.Model.Entities;
using Products.Domain.Model.Queries;
using Products.Domain.Repositories;
using Products.Domain.Services;

namespace Products.Application.Internal.QueryServices;

public class ProductDiscountQueryService(
    IProductDiscountRepository productDiscountRepository,
    IProductRepository productRepository
) : IProductDiscountQueryService
{
    public async Task<IEnumerable<ProductDiscount>> Handle(GetAllScheduledDiscountsQuery query)
    {
        return await productDiscountRepository.FindAllAsync();
    }

    public async Task<IEnumerable<Product>> Handle(GetProductsWithScheduledDiscountsQuery query)
    {
        var discounts = await productDiscountRepository.FindAllAsync();

        var productIds = discounts
            .Select(d => d.ProductId)
            .Distinct()
            .ToList();

        var products = new List<Product>();

        foreach (var productId in productIds)
        {
            var product = await productRepository.FindByIdAsync(productId);

            if (product is not null)
                products.Add(product);
        }

        return products;
    }

    public async Task<IEnumerable<ProductDiscount>> Handle(GetActiveScheduledDiscountsQuery query)
    {
        return await productDiscountRepository.FindActiveDiscountsAsync();
    }

    public async Task<IEnumerable<ProductDiscount>> Handle(GetScheduledDiscountsByProductIdQuery query)
    {
        return await productDiscountRepository.FindByProductIdAsync(query.ProductId);
    }
}