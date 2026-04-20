using Sales.Domain.Model.Aggregates;
using Sales.Domain.Model.Queries;

namespace Sales.Domain.Services;

public interface ISaleQueryService
{
    Task<IEnumerable<Sale>> Handle(GetAllSalesQuery query);
    Task<IEnumerable<Sale>> Handle(GetSalesByProductIdQuery query);
    Task<Sale?> Handle(GetSaleByIdQuery query);
}