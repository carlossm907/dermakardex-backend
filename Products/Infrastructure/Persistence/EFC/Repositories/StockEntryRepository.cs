using dermakardex_backend.Shared.Infrastructure.Persistence.EFC.Configuration;
using dermakardex_backend.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;
using Products.Domain.Model.Entities;
using Products.Domain.Model.ReadModels;
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

    public async Task<IEnumerable<ProductEntriesPerDay>> FindProductEntriesPerDayAsync(int productId, DateOnly from, DateOnly to)
    {
        return await Context.Set<StockEntry>()
        .Where(e =>
            e.ProductId == productId &&
            DateOnly.FromDateTime(e.RegisteredAt) >= from &&
            DateOnly.FromDateTime(e.RegisteredAt) <= to
        )
        .GroupBy(e => new
        {
            e.ProductId,
            Date = DateOnly.FromDateTime(e.RegisteredAt)
        })
        .Select(g => new ProductEntriesPerDay
        {
            ProductId = g.Key.ProductId,
            Date = g.Key.Date,
            Quantity = g.Sum(e => e.Quantity)
        })
        .ToListAsync();
    }

    public async Task<IEnumerable<ProductEntriesPerDay>> FindProductsEntriesPerDayAsync(DateOnly from, DateOnly to)
    {
        return await Context.Set<StockEntry>()
        .Where(e =>
            DateOnly.FromDateTime(e.RegisteredAt) >= from &&
            DateOnly.FromDateTime(e.RegisteredAt) <= to
        )
        .GroupBy(e => new
        {
            e.ProductId,
            Date = DateOnly.FromDateTime(e.RegisteredAt)
        })
        .Select(g => new ProductEntriesPerDay
        {
            ProductId = g.Key.ProductId,
            Date = g.Key.Date,
            Quantity = g.Sum(x => x.Quantity)
        })
        .ToListAsync();
    }
}