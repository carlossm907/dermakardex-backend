using Sales.Domain.Model.Aggregates;
using Sales.Domain.Model.Queries;
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

}