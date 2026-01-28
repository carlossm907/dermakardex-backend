using dermakardex_backend.Shared.Domain.Repositories;
using Products.Domain.Model.Entities;

namespace Products.Domain.Repositories;

public interface ICategoryRepository : IBaseRepository<Category>
{
    Task<IEnumerable<Category>> FindAllAsync();

    Task<bool> ExistsByIdAsync(int categoryId);
}