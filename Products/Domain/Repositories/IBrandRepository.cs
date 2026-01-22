using dermakardex_backend.Shared.Domain.Repositories;
using Products.Domain.Model.Entitites;

namespace Products.Domain.Repositories;

public interface IBrandRepository : IBaseRepository<Brand>
{
    Task<IEnumerable<Brand>> FindAllAsync();
}
