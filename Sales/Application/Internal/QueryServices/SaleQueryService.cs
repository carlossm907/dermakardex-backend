using Sales.Domain.Model.Aggregates;
using Sales.Domain.Model.Queries;
using Sales.Domain.Model.ReadModels;
using Sales.Domain.Model.ReadModels.SalesTimeLine;
using Sales.Domain.Repositories;
using Sales.Domain.Services;

namespace Sales.Application.Internal.QueryServices;

public class SaleQueryService(ISaleRepository saleRepository) : ISaleQueryService
{
    public async Task<IEnumerable<Sale>> Handle(GetAllSalesQuery query)
    {
        return await saleRepository.FindAllOrderedAsync();
    }

    public async Task<IEnumerable<Sale>> Handle(GetSalesByProductIdQuery query)
    {
        return await saleRepository.FindByProductIdAsync(query.ProductId);
    }

    public async Task<Sale?> Handle(GetSaleByIdQuery query)
    {
        return await saleRepository.FindByIdWithDetailsAsync(query.SaleId);
    }

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