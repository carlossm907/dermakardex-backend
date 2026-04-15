using Products.Domain.Model.Queries.StockEntryProductReport;
using Products.Domain.Model.ReadModels;

namespace Products.Domain.Services.QueryServices;

public interface IEntriesReportQueryService
{
    Task<IEnumerable<ProductDailyEntriesReportItem>> Handle(GetProductEntriesReportQuery query);

    Task<IEnumerable<ProductDailyEntriesReportItem>> Handle(GetProductsEntriesReportQuery query);

    Task<IEnumerable<ProductDailyEntriesReportItem>> Handle(GetAllProductsEntriesReportQuery query);

    Task<IEnumerable<ProductDailyEntriesReportItem>> Handle(GetAffectedProductsEntriesReportQuery query);
}