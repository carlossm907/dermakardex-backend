using Sales.Domain.Model.Entities;
using Sales.Interfaces.REST.Resources;

namespace Sales.Interfaces.REST.Transform;

public static class SaleItemResourceFromEntityAssembler
{
    public static SaleItemResource ToResourceFromEntity(SaleItem item)
    {
        return new SaleItemResource(
            item.ProductId,
            item.ProductName,
            item.Presentation.ToString(),
            item.Quantity,
            item.BaseUnitPrice.Amount,
            item.UnitPrice.Amount,
            item.DiscountType.ToString(),
            item.DiscountValue,
            item.LineTotal.Amount
        );
    }
}