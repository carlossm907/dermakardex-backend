using Products.Domain.Model.Queries.SalesProductReport;
using Products.Domain.Model.ReadModels;

namespace Products.Domain.Services.QueryServices;

public interface ISalesReportQueryService
{
    Task<IEnumerable<ProductSalesReportItem>> Handle(GetProductSalesReportQuery query);

    Task<IEnumerable<ProductSalesReportItem>> Handle(GetProductsSalesReportQuery query);

    Task<IEnumerable<ProductSalesReportItem>> Handle(GetAllProductsSalesReportQuery query);

    Task<IEnumerable<ProductSalesReportItem>> Handle(GetAffectedProductsSalesReportQuery query);
}