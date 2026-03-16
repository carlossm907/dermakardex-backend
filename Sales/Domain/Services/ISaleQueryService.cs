using Sales.Domain.Model.Aggregates;
using Sales.Domain.Model.Queries;
using Sales.Domain.Model.ReadModels;

namespace Sales.Domain.Services;

public interface ISaleQueryService
{
    Task<IEnumerable<Sale>> Handle(GetAllSalesQuery query);
    Task<IEnumerable<Sale>> Handle(GetSalesByCustomerDniQuery query);
    Task<IEnumerable<Sale>> Handle(GetSalesBySellerUserIdQuery query);
    Task<IEnumerable<Sale>> Handle(GetSalesByProductIdQuery query);
    Task<IEnumerable<Sale>> Handle(GetSalesByDayQuery query);
    Task<IEnumerable<Sale>> Handle(GetSalesByMonthQuery query);
    Task<Sale?> Handle(GetSaleByIdQuery query);
    Task<IEnumerable<SalesGroupedByCustomerReport>> Handle(GetSalesGroupedByCustomerByDayQuery query);
    Task<IEnumerable<SalesGroupedByCustomerReport>> Handle(GetSalesGroupedByCustomerByMonthQuery query);
    Task<IEnumerable<ProductSalesPerDay>> Handle(GetProductSalesByProductBetweenDatesQuery query);
    Task<IEnumerable<ProductSalesPerDay>> Handle(GetProductsSalesBetweenDatesQuery query);
}