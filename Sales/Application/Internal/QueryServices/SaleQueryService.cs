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

    public async Task<IEnumerable<Sale>> Handle(GetSalesByCustomerDniQuery query)
    {
        return await saleRepository.FindByCustomerDniAsync(query.CustomerDni);
    }

    public async Task<IEnumerable<Sale>> Handle(GetSalesBySellerUserIdQuery query)
    {
        return await saleRepository.FindBySellerUserIdAsync(query.SellerUserId);
    }

    public async Task<IEnumerable<Sale>> Handle(GetSalesByProductIdQuery query)
    {
        return await saleRepository.FindByProductIdAsync(query.ProductId);
    }

    public async Task<IEnumerable<Sale>> Handle(GetSalesByDayQuery query)
    {
        return await saleRepository.FindByDayAsync(query.Day);
    }

    public async Task<IEnumerable<Sale>> Handle(GetSalesByMonthQuery query)
    {
        return await saleRepository.FindByMonthAsync(query.Year, query.Month);
    }

    public async Task<Sale?> Handle(GetSaleByIdQuery query)
    {
        return await saleRepository.FindByIdAsync(query.SaleId);
    }
}