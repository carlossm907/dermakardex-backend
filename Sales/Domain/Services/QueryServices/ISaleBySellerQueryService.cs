using Sales.Domain.Model.Aggregates;
using Sales.Domain.Model.Queries;
using Sales.Domain.Model.ReadModels.SalesTimeLine;

namespace Sales.Domain.Services;

public interface ISaleBySellerQueryService
{
    Task<IEnumerable<Sale>> Handle(GetSalesBySellerUserIdQuery query);
    Task<IEnumerable<SalesTimelineByDay>> Handle(GetSalesTimelineByMonthQuery query);
}