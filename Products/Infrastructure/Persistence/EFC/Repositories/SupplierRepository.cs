using dermakardex_backend.Shared.Infrastructure.Persistence.EFC.Configuration;
using dermakardex_backend.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;
using Products.Domain.Model.Entities;
using Products.Domain.Repositories;

namespace Products.Infrastructure.Persistence.EFC.Repositories;

public class SupplierRepository(AppDbContext context) : BaseRepository<Supplier>(context), ISupplierRepository
{
    public async Task<bool> ExistsByIdAsync(int supplierId)
    {
        return await Context.Set<Supplier>()
            .AnyAsync(s => s.Id == supplierId);
    }

    public async Task<IEnumerable<Supplier>> FindAllAsync()
    => await Context.Set<Supplier>().ToListAsync();
}