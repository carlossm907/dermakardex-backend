using dermakardex_backend.Shared.Domain.Repositories;
using Products.Domain.Model.Entities;

namespace Products.Domain.Repositories;

public interface IStockEntryRepository : IBaseRepository<StockEntry>
{
    Task<IEnumerable<StockEntry>> FindByProductIdAsync(int productId);
    Task<IEnumerable<StockEntry>> FindAllAsync();
}