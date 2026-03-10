using Products.Domain.Repositories;
using Products.Interfaces.ACL;
using Products.Interfaces.ACL.Dto;
using Shared.Domain.Model.ValueObjects;

namespace Products.Application.ACL;

public class ProductsContextFacade : IProductsContextFacade
{

    private readonly IProductRepository _productRepository;
    private readonly IProductDiscountRepository _productDiscountRepository;

    public ProductsContextFacade(IProductRepository productRepository, IProductDiscountRepository productDiscountRepository)
    {
        _productRepository = productRepository;
        _productDiscountRepository = productDiscountRepository;
    }

    public async Task<ProductForSaleDto> GetProductForSaleAsync(int productId)
    {
        var product = await _productRepository.FindByIdAsync(productId)
            ?? throw new InvalidOperationException("Product not found.");

        if (!product.IsActive)
            throw new InvalidOperationException("Product is inactive.");

        var scheduledDiscount = await _productDiscountRepository.FindActiveDiscountByProductIdAsync(productId);

        DiscountType discountType;
        decimal discountValue;
        decimal finalPrice;

        if (scheduledDiscount is not null)
        {
            discountType = scheduledDiscount.Discount.Type;
            discountValue = scheduledDiscount.Discount.Value;

            var discountAmount = scheduledDiscount.Discount.CalculateDiscount(product.SalePrice);
            finalPrice = product.SalePrice.Amount - discountAmount.Amount;
        }
        else
        {
            discountType = product.Discount.Type;
            discountValue = product.Discount.Value;

            finalPrice = product.GetFinalPrice().Amount;
        }


        return new ProductForSaleDto(
            product.Id,
            product.Name,
            (int)product.Presentation,
            product.SalePrice.Amount,
            finalPrice,
            discountType,
            discountValue,
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