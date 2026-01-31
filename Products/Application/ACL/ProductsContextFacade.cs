using Products.Domain.Repositories;
using Products.Interfaces.ACL;
using Products.Interfaces.ACL.Dto;

namespace Products.Application.ACL;

public class ProductsContextFacade : IProductsContextFacade
{

    private readonly IProductRepository _productRepository;

    public ProductsContextFacade(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ProductForSaleDto> GetProductForSaleAsync(int productId)
    {
        var product = await _productRepository.FindByIdAsync(productId)
            ?? throw new InvalidOperationException("Product not found.");

        if (!product.IsActive)
            throw new InvalidOperationException("Product is inactive.");

        return new ProductForSaleDto(
            product.Id,
            product.Name,
            (int)product.Presentation,
            product.SalePrice.Amount,
            product.GetFinalPrice().Amount,
            product.Discount.Value,
            product.Discount.Type,
            product.Stock
        );
    }

    public async Task ReduceStockAsync(int productId, int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero.");

        var product = await _productRepository.FindByIdAsync(productId)
            ?? throw new InvalidOperationException("Product not found.");

        product.AdjustStock(-quantity);
    }
}