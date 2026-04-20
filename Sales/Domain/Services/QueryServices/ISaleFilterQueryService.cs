using Sales.Domain.Model.Aggregates;
using Sales.Domain.Model.Queries;

namespace Sales.Domain.Services;

public interface ISaleFilterQueryService
{
    Task<IEnumerable<Sale>> Handle(GetSalesByDayQuery query);
    Task<IEnumerable<Sale>> Handle(GetSalesByMonthQuery query);
    Task<IEnumerable<Sale>> Handle(GetSalesByCustomerDniQuery query);

}