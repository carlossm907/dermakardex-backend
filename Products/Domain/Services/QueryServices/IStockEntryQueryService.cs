using Products.Domain.Model.Entities;
using Products.Domain.Model.Queries.StockEntry;


namespace Products.Domain.Services;

public interface IStockEntryQueryService
{
    Task<IEnumerable<StockEntry>> Handle(GetProductStockEntriesQuery query);

    Task<IEnumerable<StockEntry>> Handle(GetAllStockEntriesQuery query);
}