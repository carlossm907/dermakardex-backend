using Products.Domain.Model.Aggregates;
using Products.Interfaces.REST.Resources;

namespace Products.Interfaces.REST.Transform;

public static class ProductResourceFromEntityAssembler
{
    public static ProductResource ToResourceFromEntity(Product product)
    {
        return new ProductResource(
            product.Id,
            product.Code,
            product.Name,
            product.BrandId,
            product.CategoryId,
            product.LaboratoryId,
            product.SupplierId,
            product.Presentation.ToString(),
            product.PurchasePrice.Amount,
            product.GetFinalPrice().Amount,
            product.SalePrice.Amount,
            product.MaxDiscountAmount.Amount,
            product.Discount.Type,
            product.Discount.Value,
            product.Stock,
            product.StockAlertThreshold,
            product.IsActive
        );
    }
}
