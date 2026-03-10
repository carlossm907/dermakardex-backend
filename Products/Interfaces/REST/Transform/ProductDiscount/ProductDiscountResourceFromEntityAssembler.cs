using Products.Domain.Model.Entities;
using Products.Interfaces.REST.Resources;

namespace Products.Interfaces.REST.Transform;

public static class ProductDiscountResourceFromEntityAssembler
{
    public static ProductDiscountResource ToResourceFromEntity(ProductDiscount entity)
    {
        return new ProductDiscountResource(
            entity.Id,
            entity.ProductId,
            entity.Name,
            entity.Discount.Type.ToString(),
            entity.Discount.Value,
            entity.StartsAt,
            entity.EndsAt,
            entity.IsActive
        );
    }
}