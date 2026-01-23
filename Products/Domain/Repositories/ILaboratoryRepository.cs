using dermakardex_backend.Shared.Domain.Repositories;
using Products.Domain.Model.Entities;

namespace Products.Domain.Repositories;

public interface ILaboratoryRepository : IBaseRepository<Laboratory>
{
    Task<IEnumerable<Laboratory>> FindAllAsync();

    Task<bool> ExistsByIdAsync(int laboratoryId);
}