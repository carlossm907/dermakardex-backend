using Products.Domain.Model.Entities;
using Products.Domain.Model.Queries.StockEntry;
using Products.Domain.Repositories;
using Products.Domain.Services;

namespace Products.Application.Internal.QueryServices;

public class StockEntryQueryService(IStockEntryRepository stockEntryRepository) : IStockEntryQueryService
{
    public async Task<IEnumerable<StockEntry>> Handle(GetProductStockEntriesQuery query)
    {
        return await stockEntryRepository.FindByProductIdAsync(query.ProductId);
    }

    public async Task<IEnumerable<StockEntry>> Handle(GetAllStockEntriesQuery query)
    {
        return await stockEntryRepository.FindAllAsync();
    }
}