using dermakardex_backend.Shared.Domain.Repositories;
using Products.Domain.Model.Entities;

namespace Products.Domain.Repositoies;

public interface ICategoryRepository : IBaseRepository<Category>
{
    Task<IEnumerable<Category>> FindAllAsync();
}