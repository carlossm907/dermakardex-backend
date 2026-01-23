using dermakardex_backend.Shared.Infrastructure.Persistence.EFC.Configuration;
using dermakardex_backend.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;
using Products.Domain.Model.Entities;
using Products.Domain.Repositories;

namespace Products.Infrastructure.Persistence.EFC.Repositories;

public class LaboratoryRepository(AppDbContext context) : BaseRepository<Laboratory>(context), ILaboratoryRepository
{
    public async Task<IEnumerable<Laboratory>> FindAllAsync()
        => await Context.Set<Laboratory>().ToListAsync();
}