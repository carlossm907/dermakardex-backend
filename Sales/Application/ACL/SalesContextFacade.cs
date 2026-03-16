using Sales.Domain.Model.Queries;
using Sales.Domain.Model.ReadModels;
using Sales.Domain.Services;
using Sales.Interfaces.ACL;

namespace Sales.Application.ACL;

public class SalesContextFacade(
    ISaleQueryService saleQueryService
) : ISalesContextFacade
{
    public async Task<IEnumerable<ProductSalesPerDay>> FetchProductSalesPerDay(int productId, DateOnly from, DateOnly to)
    {
        var query = new GetProductSalesByProductBetweenDatesQuery(
            productId,
            from,
            to
        );

        var result = await saleQueryService.Handle(query);

        return result;
    }

    public async Task<IEnumerable<ProductSalesPerDay>> FetchProductsSalesPerDay(DateOnly from, DateOnly to)
    {
        var query = new GetProductsSalesBetweenDatesQuery(
            from,
            to
        );

        var result = await saleQueryService.Handle(query);

        return result;
    }
}