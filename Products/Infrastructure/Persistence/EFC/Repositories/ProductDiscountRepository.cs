using dermakardex_backend.Shared.Infrastructure.Persistence.EFC.Configuration;
using dermakardex_backend.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;
using Products.Domain.Model.Entities;
using Products.Domain.Repositories;

namespace Products.Infrastructure.Persistence.EFC.Repositories;

public class ProductDiscountRepository(AppDbContext context) : BaseRepository<ProductDiscount>(context), IProductDiscountRepository
{
    public async Task<bool> ExistsOverlapAsync(int productId, DateTime startsAt, DateTime endsAt)
    {
        return await Context.Set<ProductDiscount>()
            .AnyAsync(d =>
                d.ProductId == productId &&
                d.IsActive &&
                d.StartsAt < endsAt &&
                d.EndsAt > startsAt);
    }

    public async Task<ProductDiscount?> FindActiveDiscountByProductIdAsync(int productId)
    {
        var now = DateTime.UtcNow;

        return await Context.Set<ProductDiscount>()
            .Where(d =>
                d.ProductId == productId &&
                d.IsActive &&
                d.StartsAt <= now &&
                d.EndsAt >= now)
            .OrderByDescending(d => d.StartsAt)
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<ProductDiscount>> FindActiveDiscountsAsync()
    {
        var now = DateTime.UtcNow;

        return await Context.Set<ProductDiscount>()
            .Where(d => d.IsActive &&
                        d.StartsAt <= now &&
                        d.EndsAt >= now)
            .ToListAsync();
    }

    public async Task<IEnumerable<ProductDiscount>> FindAllAsync()
    {
        return await Context.Set<ProductDiscount>()
            .OrderByDescending(d => d.StartsAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<ProductDiscount>> FindByProductIdAsync(int productId)
    {
        return await Context.Set<ProductDiscount>()
            .Where(d => d.ProductId == productId)
            .OrderByDescending(d => d.StartsAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<ProductDiscount>> FindExpiredDiscountsAsync()
    {
        var now = DateTime.UtcNow;

        return await Context.Set<ProductDiscount>()
            .Where(d => d.IsActive && d.EndsAt < now)
            .ToListAsync();
    }
}