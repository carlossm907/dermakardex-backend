using dermakardex_backend.Shared.Domain.Repositories;
using Products.Domain.Model.Entities;
using Products.Domain.Model.ReadModels;

namespace Products.Domain.Repositories;

public interface IStockEntryRepository : IBaseRepository<StockEntry>
{
    Task<IEnumerable<StockEntry>> FindByProductIdAsync(int productId);
    Task<IEnumerable<StockEntry>> FindAllAsync();
    Task<IEnumerable<ProductEntriesPerDay>> FindProductEntriesPerDayAsync(int productId, DateOnly from, DateOnly to);
    Task<IEnumerable<ProductEntriesPerDay>> FindProductsEntriesPerDayAsync(DateOnly from, DateOnly to);

}