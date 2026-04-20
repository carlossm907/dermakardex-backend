using Sales.Domain.Model.Queries;
using Sales.Domain.Model.ReadModels;

namespace Sales.Domain.Services;

public interface ISaleForStockReportQuery
{
    Task<IEnumerable<ProductSalesPerDay>> Handle(GetProductSalesByProductBetweenDatesQuery query);
    Task<IEnumerable<ProductSalesPerDay>> Handle(GetProductsSalesBetweenDatesQuery query);
}