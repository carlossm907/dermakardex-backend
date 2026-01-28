using dermakardex_backend.Shared.Infrastructure.Persistence.EFC.Configuration;
using dermakardex_backend.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;
using Products.Domain.Model.Aggregates;
using Products.Domain.Repositories;

namespace Products.Infrastructure.Persistence.EFC.Repositories;

public class ProductRepository(AppDbContext context) : BaseRepository<Product>(context), IProductRepository
{
    public async Task<IEnumerable<Product>> FindLowStockAsync()

        => await Context.Set<Product>()
            .Where(p => p.Stock <= p.StockAlertThreshold)
            .ToListAsync();

    public async Task<IEnumerable<Product>> ListAsync(string? Name)
    {
        var query = Context.Set<Product>().AsQueryable();

        if (!string.IsNullOrWhiteSpace(Name))
            query = query.Where(p =>
            EF.Functions.ILike(p.Name, $"%{Name.Trim()}%"));

        return await query.ToListAsync();
    }
}