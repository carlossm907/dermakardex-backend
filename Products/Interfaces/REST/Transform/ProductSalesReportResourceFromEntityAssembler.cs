using Products.Domain.Model.ReadModels;
using Products.Interfaces.REST.Resources;

namespace Products.Interfaces.REST.Transform;

public static class ProductSalesReportResourceFromEntityAssembler
{
    public static ProductSalesReportResource ToResourceFromEntity(ProductSalesReportItem entity)
    {
        return new ProductSalesReportResource(
            entity.ProductId,
            entity.ProductName,
            entity.From,
            entity.To,
            entity.Quantity
        );
    }
}