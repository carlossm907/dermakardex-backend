using dermakardex_backend.Shared.Infrastructure.Persistence.EFC.Configuration;
using dermakardex_backend.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;
using Products.Domain.Model.Entitites;
using Products.Domain.Repositories;

namespace Products.Infrastructure.Persistence.EFC.Repositories;

public class BrandRepository(AppDbContext context) : BaseRepository<Brand>(context), IBrandRepository
{
    public async Task<bool> ExistsByIdAsync(int brandId)
    {
        return await Context.Set<Brand>()
            .AnyAsync(b => b.Id == brandId);
    }

    public async Task<IEnumerable<Brand>> FindAllAsync()

    => await Context.Set<Brand>().ToListAsync();

}