using Products.Domain.Model.ReadModels;
using Products.Interfaces.REST.Resources;

namespace Products.Interfaces.REST.Transform;

public static class ProductDailyStockReportResourceFromEntityAssembler
{
    public static ProductDailyStockReportResource ToResourceFromEntity(ProductDailyStockReportItem entity)
    {
        return new ProductDailyStockReportResource(
            entity.ProductId,
            entity.ProductName,
            entity.Date,
            entity.InitialStock,
            entity.Entries,
            entity.Sold,
            entity.FinalStock
        );
    }
}