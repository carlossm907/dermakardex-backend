using dermakardex_backend.Shared.Infrastructure.Persistence.EFC.Configuration;
using dermakardex_backend.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;
using Products.Domain.Model.Entities;
using Products.Domain.Repositories;

namespace Products.Infrastructure.Persistence.EFC.Repositories;

public class StockEntryRepository(AppDbContext context) : BaseRepository<StockEntry>(context), IStockEntryRepository
{
    public async Task<IEnumerable<StockEntry>> FindAllAsync()
    {
        return await Context.Set<StockEntry>()
                .OrderByDescending(e => e.RegisteredAt)
                .ToListAsync();
    }

    public async Task<IEnumerable<StockEntry>> FindByProductIdAsync(int productId)
    {
        return await Context.Set<StockEntry>()
                .Where(e => e.ProductId == productId)
                .OrderByDescending(e => e.RegisteredAt)
                .ToListAsync();
    }
}