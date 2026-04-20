using Sales.Domain.Model.Queries;
using Sales.Domain.Model.ReadModels;

namespace Sales.Domain.Services;

public interface ISaleByClientQueryService
{
    Task<IEnumerable<SalesGroupedByCustomerReport>> Handle(GetSalesGroupedByCustomerByDayQuery query);
    Task<IEnumerable<SalesGroupedByCustomerReport>> Handle(GetSalesGroupedByCustomerByMonthQuery query);
}