using dermakardex_backend.Shared.Domain.Repositories;
using Products.Domain.Model.Entities;

namespace Products.Domain.Repositories;

public interface IBrandRepository : IBaseRepository<Brand>
{
    Task<IEnumerable<Brand>> FindAllAsync();

    Task<bool> ExistsByIdAsync(int brandId);
}
