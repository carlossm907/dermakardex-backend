using Sales.Domain.Model.Queries;
using Sales.Domain.Model.ReadModels;
using Sales.Domain.Repositories;
using Sales.Domain.Services;

namespace Sales.Application.Internal.QueryServices;

public class SaleForStockReportQuery(

    ISaleRepository saleRepository

) : ISaleForStockReportQuery
{
    public async Task<IEnumerable<ProductSalesPerDay>> Handle(GetProductSalesByProductBetweenDatesQuery query)
    {
        return await saleRepository.FindProductSalesPerDayAsync(
        query.ProductId,
        query.From,
        query.To
    );

    }

    public async Task<IEnumerable<ProductSalesPerDay>> Handle(GetProductsSalesBetweenDatesQuery query)
    {
        return await saleRepository.FindProductsSalesPerDayAsync(
        query.From,
        query.To
    );
    }
}