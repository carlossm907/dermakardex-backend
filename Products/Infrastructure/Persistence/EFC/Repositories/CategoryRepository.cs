using dermakardex_backend.Shared.Infrastructure.Persistence.EFC.Configuration;
using dermakardex_backend.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;
using Products.Domain.Model.Entities;
using Products.Domain.Repositories;

namespace Products.Infrastructure.Persistence.EFC.Repositories;

public class CategoryRepository(AppDbContext context) : BaseRepository<Category>(context), ICategoryRepository
{
    public async Task<bool> ExistsByIdAsync(int categoryId)
    {
        return await Context.Set<Category>()
            .AnyAsync(c => c.Id == categoryId);
    }

    public async Task<IEnumerable<Category>> FindAllAsync()
        => await Context.Set<Category>().ToListAsync();
}