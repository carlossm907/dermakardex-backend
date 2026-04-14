using Products.Domain.Model.ReadModels;
using Products.Interfaces.REST.Resources.Reports;

namespace Products.Interfaces.REST.Transform;

public static class ProductEntriesReportResourceFromEntityAssembler
{
    public static ProductEntriesReportResource ToResourceFromEntity(ProductDailyEntriesReportItem entity)
    {
        return new ProductEntriesReportResource(
            entity.ProductId,
            entity.ProductName,
            entity.From,
            entity.To,
            entity.Quantity
        );
    }
}