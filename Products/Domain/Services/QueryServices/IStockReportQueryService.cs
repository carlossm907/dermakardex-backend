using Products.Domain.Model.Queries.StockReport;
using Products.Domain.Model.ReadModels;

namespace Products.Domain.Services.QueryServices;

public interface IStockReportQueryService
{
    Task<IEnumerable<ProductDailyStockReportItem>> Handle(GetProductDailyStockReportQuery query);

    Task<IEnumerable<ProductDailyStockReportItem>> Handle(GetAllProductsDailyStockReportQuery query);

    Task<IEnumerable<ProductDailyStockReportItem>> Handle(GetProductsDailyStockReportQuery query);

    Task<IEnumerable<ProductDailyStockReportItem>> Handle(GetAffectedProductsDailyStockReportQuery query);
}