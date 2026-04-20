using Sales.Domain.Model.Queries;
using Sales.Domain.Model.ReadModels;

namespace Sales.Domain.Services;

public interface ISaleForStockReportQueryService
{
    Task<IEnumerable<ProductSalesPerDay>> Handle(GetProductSalesByProductBetweenDatesQuery query);
    Task<IEnumerable<ProductSalesPerDay>> Handle(GetProductsSalesBetweenDatesQuery query);
}