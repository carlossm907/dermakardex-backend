using Products.Domain.Model.Aggregates;
using Products.Interfaces.REST.Resources;

namespace Products.Interfaces.REST.Transform;

public static class ProductResourceFromEntityAssembler
{
    public static ProductResource ToResourceFromEntity(Product product)
    {
        return new ProductResource(
            product.Id,
            product.Name,
            product.BrandId,
            product.CategoryId,
            product.LaboratoryId,
            product.SupplierId,
            product.Presentation.ToString(),
            product.PurchasePrice.Amount,
            product.SalePrice.Amount,
            product.MaxDiscountAmount.Amount,
            product.Stock,
            product.IsActive
        );
    }
}
