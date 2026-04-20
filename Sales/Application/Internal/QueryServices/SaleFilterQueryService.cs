using Sales.Domain.Model.Aggregates;
using Sales.Domain.Model.Queries;
using Sales.Domain.Repositories;
using Sales.Domain.Services;

namespace Sales.Application.Internal.QueryServices;

public class SaleFilterQueryService(

    ISaleRepository saleRepository

) : ISaleFilterQueryService
{
    public async Task<IEnumerable<Sale>> Handle(GetSalesByCustomerDniQuery query)
    {
        return await saleRepository.FindByCustomerDniAsync(query.CustomerDni);
    }

    public async Task<IEnumerable<Sale>> Handle(GetSalesByDayQuery query)
    {
        return await saleRepository.FindByDayAsync(query.Day);
    }

    public async Task<IEnumerable<Sale>> Handle(GetSalesByMonthQuery query)
    {
        return await saleRepository.FindByMonthAsync(query.Year, query.Month);
    }
}