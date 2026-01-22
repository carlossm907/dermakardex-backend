using dermakardex_backend.Shared.Domain.Repositories;
using Products.Domain.Model.Entities;

namespace Products.Domain.Repositories;

public interface ISupplierRepository : IBaseRepository<Supplier>
{
    Task<IEnumerable<Supplier>> FindAllAsync();
}